using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddStepNavigatorToStep;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Xunit;

namespace Application.IntegrationTests.Commands.AddStepNavigatorToStep
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddStepNavigatorToStepCommandHandlerTests : CommandTestBase
    {
        public AddStepNavigatorToStepCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }
        
        [Fact]
        public async Task When_InvalidCommandPassed_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            //Act + Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await mediator!.Send(new AddStepNavigatorToStepCommand
                {
                    StepId = Guid.Empty,
                    TargetStepId = Guid.Empty
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_ValidCommandPassed_Expect_StepNavigatorToBeCreatedInStep()
        {
            //Arrange
            var step = new Step("step");
            var targetStep = new Step("targetStep");
            
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            await Context.AddAsync(step);
            await Context.AddAsync(targetStep);
            await Context.SaveChangesAsync();

            //Act
            await mediator!.Send(new AddStepNavigatorToStepCommand
            {
                StepId = step.Id,
                TargetStepId = targetStep.Id
            }, CancellationToken.None);
            
            //Assert
            var stepRepository =
                ApplicationFixture.Host.Services.GetService(typeof(IStepRepository)) as IStepRepository;

            var stepFromDb = await stepRepository!.GetByIdAsync(step.Id);

            stepFromDb.StepNavigators.Count().Should().Be(1);
            stepFromDb.StepNavigators.First().TargetStep.Id.Should().Be(targetStep.Id);
        }
    }
}