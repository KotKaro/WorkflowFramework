using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.CompareExpectation.AddEqualExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.IntegrationTests.Commands.CompareExpectation.CompareExpectation.AddEqualExpectation
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddEqualExpectationCommandHandlerTests : CommandTestBase
    {
        public AddEqualExpectationCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }
        
        
        [Fact]
        public async Task When_CommandIsInvalid_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            //Act + Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await mediator!.Send(new AddEqualExpectationCommand
                {
                    StepNavigatorId = Guid.Empty,
                    ValueProviderId = Guid.Empty,
                    Value = "test"
                });
            });
        }
        
        [Fact]
        public async Task When_CommandIsValid_Expect_StepNavigatorGotExpectation()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var typeMetadata = new TypeMetadata(typeof(TestClass));
            var step = new Step("test");
            var stepNavigator = new StepNavigator(step);

            await Context.AddAsync(typeMetadata);
            await Context.AddAsync(step);
            await Context.AddAsync(stepNavigator);
            await Context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new AddEqualExpectationCommand
            {
                StepNavigatorId = stepNavigator.Id,
                ValueProviderId = typeMetadata.ValueProviders.First().Id,
                Value = "test"
            });
            
            //Assert
            var stepNavigatorFromDb = await Context.Set<StepNavigator>().FirstAsync(x => x.Id == stepNavigator.Id);

            stepNavigatorFromDb.Expectations.Count().Should().Be(1);

            var equalExpectation = stepNavigatorFromDb.Expectations.First() as EqualExpectation;
            equalExpectation!.DescribedType.Should().Be(typeof(TestClass));
            equalExpectation.Value.GetOriginalValue().Should().Be("test");
        }
    }
}