using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CreateStep;
using Application.Commands.RemoveStep;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.RemoveStep
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class RemoveStepCommandHandlerTests : CommandTestBase
    {
        public RemoveStepCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
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
                await mediator!.Send(new RemoveStepCommand
                {
                    StepId = Guid.Empty
                });
            });
        }
        
        [Fact]
        public async Task When_ExistingStepIdProvided_Expect_StepWithSuchIdNoLongerExistsInDb()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;
            
            await mediator!.Send(new CreateStepCommand
            {
                StepName = "test"
            });

            var step = context!.Set<Step>().First(x => x.Name.Value == "test");
            
            //Act
            await mediator!.Send(new RemoveStepCommand
            {
                StepId = step.Id
            });
            
            //Assert
            context!.Set<Step>().Count().Should().Be(0);
        }
    }
}