using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.RemoveExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.IntegrationTests.Commands.RemoveExpectation
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class RemoveExpectationCommandHandlerTests : CommandTestBase
    {
        public RemoveExpectationCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
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
                await mediator!.Send(new RemoveExpectationCommand
                {
                    ExpectationId = Guid.Empty
                });
            });
        }
        
        [Fact]
        public async Task When_CommandIsValid_Expect_StepNavigatorGotRemovedExpectation()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var typeMetadata = new TypeMetadata(typeof(TestClass));
            var step = new Step("test");
            var stepNavigator = new StepNavigator(step);
            var expectation1 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");
            var expectation2 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");
            
            stepNavigator.AddExpectations(expectation1, expectation2);

            await Context.AddAsync(typeMetadata);
            await Context.AddAsync(step);
            await Context.AddAsync(stepNavigator);
            await Context.AddAsync(expectation1);
            await Context.AddAsync(expectation2);
            await Context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new RemoveExpectationCommand
            {
                ExpectationId = expectation1.Id
            });
            
            //Assert
            var stepNavigatorFromDb = await Context
                .Set<StepNavigator>()
                .Include(x => x.Expectations)
                .FirstAsync(x => x.Id == stepNavigator.Id);
            
            stepNavigatorFromDb.Expectations.Count().Should().Be(1);
        }
        
        [Fact]
        public async Task When_CommandIsValid_Expect_ExpectationIsRemovedFromDatabase()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var typeMetadata = new TypeMetadata(typeof(TestClass));
            var step = new Step("test");
            var stepNavigator = new StepNavigator(step);
            var expectation1 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");
            var expectation2 = new EqualExpectation(typeMetadata.ValueProviders.First(), "test");
            
            stepNavigator.AddExpectations(expectation1, expectation2);

            await Context.AddAsync(typeMetadata);
            await Context.AddAsync(step);
            await Context.AddAsync(stepNavigator);
            await Context.AddAsync(expectation1);
            await Context.AddAsync(expectation2);
            await Context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new RemoveExpectationCommand
            {
                ExpectationId = expectation1.Id
            });
            
            //Assert
            Context.Set<Expectation>().FirstOrDefault(x => x.Id == expectation1.Id).Should().BeNull();
        }
    }
}