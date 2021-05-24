using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.CreateProcess
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class CreateProcessCommandHandlerTests : CommandTestBase
    {
        public CreateProcessCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }
        
        [Fact]
        public void When_IncorrectDataProvided_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            //Act + Assert
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await mediator!.Send(new Application.Commands.CreateProcess.CreateProcessCommand
                {
                    ProcessName = ""
                });
            });
        }
        
        [Fact]
        public async Task When_CorrectDataProvided_Expect_TypeMetadataAddedToDatabase()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

            //Act
            await mediator!.Send(new Application.Commands.CreateProcess.CreateProcessCommand
            {
                ProcessName = "test"
            });
            
            //Assert
            context!.Set<Process>().Count().Should().Be(1);
        }
        
        [Fact]
        public async Task When_ProcessWithSameNameTryingToCreateTwice_Expect_ErrorAtSecondTime()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

            //Act
            await mediator!.Send(new Application.Commands.CreateProcess.CreateProcessCommand
            {
                ProcessName = "test"
            });

            
            //Assert
            await Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
            {
                await mediator!.Send(new Application.Commands.CreateProcess.CreateProcessCommand
                {
                    ProcessName = "test"
                });
            });
            
            context!.Set<Process>().Count().Should().Be(1);
        }
    }
}