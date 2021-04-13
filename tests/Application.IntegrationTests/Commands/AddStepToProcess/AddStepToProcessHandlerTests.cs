using System.Linq;
using System.Threading.Tasks;
using Application.Commands.AddStepToProcess;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.AddStepToProcess
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddStepToProcessHandlerTests
    {
        private readonly ApplicationFixture _applicationFixture;
        private readonly WorkflowFrameworkDbContext _context;

        public AddStepToProcessHandlerTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;

            _context =
                _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as
                    WorkflowFrameworkDbContext;

            _context!.Set<Process>().RemoveRange(_context!.Set<Process>());
            _context!.Set<Step>().RemoveRange(_context!.Set<Step>());
            _context!.SaveChanges();
        }


        [Fact]
        public async Task When_ProcessNotContainsStep_Expect_StepShouldBePresentInProcessSteps()
        {
            //Arrange
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var processRepository = _applicationFixture.Host.Services.GetService(typeof(IProcessRepository)) as IProcessRepository;
            
            var process = new Process("test");
            var step = new Step("test");
            
            await _context.Set<Process>().AddAsync(process);
            await _context.Set<Step>().AddAsync(step);
            await _context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new AddStepToProcessCommand
            {
                ProcessId = process.Id,
                StepId = step.Id
            });

            //Assert
            var processFromDb = await processRepository!.GetByIdAsync(process.Id);
            processFromDb.Steps.Count().Should().Be(1);
        }
    }
}