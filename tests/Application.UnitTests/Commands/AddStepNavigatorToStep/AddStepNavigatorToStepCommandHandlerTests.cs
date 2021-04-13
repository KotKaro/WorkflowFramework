using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddStepNavigatorToStep;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddStepNavigatorToStep
{
    public class AddStepNavigatorToStepCommandHandlerTests
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
            
            var sut = new AddStepNavigatorToStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddStepNavigatorToStepCommand
                {
                    StepId = stepId,
                    TargetStepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public void When_TargetStepDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var stepId = Guid.NewGuid();
            var targetStepId = Guid.NewGuid();
            
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            stepRepositoryMock.Setup(x => x.GetByIdAsync(stepId))
                .ReturnsAsync(new Step("test"));
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(targetStepId))
                .ReturnsAsync(null as Step);
            
            var sut = new AddStepNavigatorToStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddStepNavigatorToStepCommand
                {
                    StepId = stepId,
                    TargetStepId = targetStepId
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_StepAlreadyContainsStepNavigatorWithTargetStepId_Expect_StepNavigatorIsNotAdded()
        {
            //Arrange
            var stepId = Guid.NewGuid();
            var targetStepId = Guid.NewGuid();
            
            var stepRepositoryMock = new Mock<IStepRepository>();
            var stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();

            var step = new Step("test");
            var targetStep = new Step("targetStep");
            
            step.AddStepNavigators(new StepNavigator(targetStep));

            stepRepositoryMock.Setup(x => x.GetByIdAsync(stepId))
                .ReturnsAsync(step);
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(targetStepId))
                .ReturnsAsync(targetStep);
            
            var sut = new AddStepNavigatorToStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );

            //Act
            await sut.Handle(new AddStepNavigatorToStepCommand
            {
                StepId = stepId,
                TargetStepId = targetStepId
            }, CancellationToken.None);
            
            //Assert
            step.StepNavigators.Count().Should().Be(1);
        }
        
        [Fact]
        public async Task When_StepNotContainStepNavigatorWithTargetStepId_Expect_StepNavigatorIsAdded()
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
            
            var sut = new AddStepNavigatorToStepCommandHandler(
                stepRepositoryMock.Object,
                stepNavigatorRepositoryMock.Object
            );
            
            //Act
            await sut.Handle(new AddStepNavigatorToStepCommand
            {
                StepId = stepId,
                TargetStepId = targetStepId
            }, CancellationToken.None);
            
            //Assert
            step.StepNavigators.Count().Should().Be(1);
        }
    }
}