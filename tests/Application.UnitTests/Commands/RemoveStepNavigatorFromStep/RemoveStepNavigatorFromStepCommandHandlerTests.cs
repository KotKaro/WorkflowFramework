using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.RemoveStepNavigatorFromStep;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStepNavigatorFromStep
{
    public class RemoveStepNavigatorFromStepCommandHandlerTests
    {
        [Fact]
        public void When_StepDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var stepId = Guid.NewGuid();
            
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            stepRepositoryMock.Setup(x => x.GetByIdAsync(stepId))
                .ReturnsAsync(null as Step);
            
            var sut = new RemoveStepNavigatorFromStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new RemoveStepNavigatorFromStepCommand
                {
                    StepId = stepId,
                    TargetStepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_StepContainsStepNavigatorWithTargetStepId_Expect_StepNavigatorIsRemoved()
        {
            //Arrange
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            var step = new Step("test");
            var targetStep = new Step("targetStep");
            
            step.AddStepNavigators(new StepNavigator(targetStep));

            stepRepositoryMock.Setup(x => x.GetByIdAsync(step.Id))
                .ReturnsAsync(step);
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(targetStep.Id))
                .ReturnsAsync(targetStep);
            
            var sut = new RemoveStepNavigatorFromStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act
            await sut.Handle(new RemoveStepNavigatorFromStepCommand
            {
                StepId = step.Id,
                TargetStepId = targetStep.Id
            }, CancellationToken.None);
            
            //Assert
            step.StepNavigators.Count().Should().Be(0);
        }
        
        [Fact]
        public async Task When_StepNotContainStepNavigatorWithTargetStepId_Expect_StepNavigatorIsNotRemoved()
        {
            //Arrange
            var stepId = Guid.NewGuid();
            var targetStepId = Guid.NewGuid();
            
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            var step = new Step("test");
            var targetStep = new Step("targetStep");

            stepRepositoryMock.Setup(x => x.GetByIdAsync(stepId))
                .ReturnsAsync(step);
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(targetStepId))
                .ReturnsAsync(targetStep);
            
            var sut = new RemoveStepNavigatorFromStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );
            
            //Act
            await sut.Handle(new RemoveStepNavigatorFromStepCommand
            {
                StepId = stepId,
                TargetStepId = targetStepId
            }, CancellationToken.None);
            
            //Assert
            step.StepNavigators.Count().Should().Be(0);
        }
        
        [Fact]
        public async Task When_StepContainsStepNavigatorWithTargetStepId_Expect_StepNavigatorRepositoryRemoveCalledOnce()
        {
            //Arrange
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            var step = new Step("test");
            var targetStep = new Step("targetStep");
            
            step.AddStepNavigators(new StepNavigator(targetStep));

            stepRepositoryMock.Setup(x => x.GetByIdAsync(step.Id))
                .ReturnsAsync(step);
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(targetStep.Id))
                .ReturnsAsync(targetStep);
            
            var sut = new RemoveStepNavigatorFromStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act
            await sut.Handle(new RemoveStepNavigatorFromStepCommand
            {
                StepId = step.Id,
                TargetStepId = targetStep.Id
            }, CancellationToken.None);
            
            //Assert
            stepNavigatorRepositoryMock.Verify(x => x.Remove(
                It.IsAny<StepNavigator>()
                ), Times.Once()
            );
        }
    }
}