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
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.AddStepNavigatorToStep
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddStepNavigatorToStepCommandHandlerTests
    {
        private readonly ApplicationFixture _applicationFixture;
        private readonly WorkflowFrameworkDbContext _context;
        
        public AddStepNavigatorToStepCommandHandlerTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;

            _context =
                _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as
                    WorkflowFrameworkDbContext;

            _context!.Set<StepNavigator>().RemoveRange(_context!.Set<StepNavigator>());
            _context!.Set<Step>().RemoveRange(_context!.Set<Step>());
            _context!.SaveChanges();
        }
        
        [Fact]
        public async Task When_InvalidCommandPassed_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

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
            
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            await _context.AddAsync(step);
            await _context.AddAsync(targetStep);
            await _context.SaveChangesAsync();

            //Act
            await mediator!.Send(new AddStepNavigatorToStepCommand
            {
                StepId = step.Id,
                TargetStepId = targetStep.Id
            }, CancellationToken.None);
            
            //Assert
            var stepRepository =
                _applicationFixture.Host.Services.GetService(typeof(IStepRepository)) as IStepRepository;

            var stepFromDb = await stepRepository!.GetByIdAsync(step.Id);

            stepFromDb.StepNavigators.Count().Should().Be(1);
            stepFromDb.StepNavigators.First().TargetStep.Id.Should().Be(targetStep.Id);
        }
    }
}