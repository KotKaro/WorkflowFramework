using System.Linq;
using System.Threading.Tasks;
using Application.Commands.AddStepToProcess;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Application.IntegrationTests.Commands.AddStepToProcess
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddStepToProcessHandlerTests : CommandTestBase
    {
        public AddStepToProcessHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }


        [Fact]
        public async Task When_ProcessNotContainsStep_Expect_StepShouldBePresentInProcessSteps()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var processRepository = ApplicationFixture.Host.Services.GetService(typeof(IProcessRepository)) as IProcessRepository;
            
            var process = new Process("test");
            var step = new Step("test");
            
            await Context.Set<Process>().AddAsync(process);
            await Context.Set<Step>().AddAsync(step);
            await Context.SaveChangesAsync();
            
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