using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateStep;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.CreateStep
{
    public class CreateStepCommandHandlerTests
    {
        [Fact]
        public async Task When_StepWithNameAlreadyExists_Expect_StepWithNameExistsExceptionThrown()
        {
            //Arrange
            var stepRepositoryMock = new Mock<IStepRepository>();
            stepRepositoryMock.Setup(x => x.GetByNameAsync("test"))
                .ThrowsAsync(new StepWithNameExistsException("test"));

            var sut = new CreateStepCommandHandler(stepRepositoryMock.Object);
            
            //Act + Assert
            await Assert.ThrowsAsync<StepWithNameExistsException>(async () =>
            {
                await sut.Handle(new Application.Commands.CreateStep.CreateStepCommand
                {
                    StepName = "test"
                }, CancellationToken.None);
            });
        }
    }
}