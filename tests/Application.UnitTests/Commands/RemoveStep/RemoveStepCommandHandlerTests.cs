using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.RemoveStep;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStep
{
    public class RemoveStepCommandHandlerTests
    {
        [Fact]
        public async Task When_StepWithProvidedIdDoesNotExists_Expect_RepositoryRemoveIsNotCalled()
        {
            //Arrange
            var stepRepositoryMock = new Mock<IStepRepository>();
            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Step);
            
            var sut = new RemoveStepCommandHandler(stepRepositoryMock.Object);

            //Act
            await sut.Handle(new RemoveStepCommand
            {
                StepId = Guid.NewGuid()
            }, CancellationToken.None);
            
            //Assert
            stepRepositoryMock.Verify(x => x.Remove(It.IsAny<Step>()), Times.Never);
        }
    }
}