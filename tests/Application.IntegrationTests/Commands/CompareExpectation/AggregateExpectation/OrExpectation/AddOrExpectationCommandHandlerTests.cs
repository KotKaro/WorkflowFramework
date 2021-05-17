using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.AggregateExpectation.AddOrExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.IntegrationTests.Commands.CompareExpectation.AggregateExpectation.OrExpectation
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddOrExpectationCommandHandlerTests : CommandTestBase
    {
        public AddOrExpectationCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
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
                await mediator!.Send(new AddOrExpectationCommand
                {
                    StepNavigatorId = Guid.Empty,
                    ExpectationIds = new []
                    {
                        Guid.Empty
                    }
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
            var expectation1 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");
            var expectation2 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");

            await Context.AddAsync(typeMetadata);
            await Context.AddAsync(step);
            await Context.AddAsync(stepNavigator);
            await Context.AddAsync(expectation1);
            await Context.AddAsync(expectation2);
            await Context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new AddOrExpectationCommand
            {
                StepNavigatorId = stepNavigator.Id,
                ExpectationIds = new []
                {
                    expectation1.Id,
                    expectation2.Id
                }
            });
            
            //Assert
            var stepNavigatorFromDb = await Context.Set<StepNavigator>()
                .Include(x => x.Expectations)
                .FirstAsync(x => x.Id == stepNavigator.Id);

            stepNavigatorFromDb.Expectations.Count().Should().Be(1);

            var orExpectation = stepNavigatorFromDb.Expectations.First() as Domain.ProcessAggregate.Expectations.AggregateExpectations.OrExpectation;
            orExpectation!.Expectations.Count.Should().Be(2);
        }
    }
}