using System.Linq;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.CreateProcess
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class CreateProcessCommandHandlerTests
    {
        private readonly ApplicationFixture _applicationFixture;

        public CreateProcessCommandHandlerTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;
            
            var context = _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

            context!.Set<Process>().RemoveRange( context!.Set<Process>());
            context!.SaveChanges();  
        }
        
        [Fact]
        public void When_IncorrectDataProvided_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

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
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

            //Act
            await mediator!.Send(new Application.Commands.CreateProcess.CreateProcessCommand
            {
                ProcessName = "test"
            });
            
            //Assert
            context!.Set<Process>().Count().Should().Be(1);
        }
    }
}