using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.RemoveStepFromProcess;
using Common.Tests;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStepFromProcess
{
    public class RemoveStepFromProcessHandlerTests
    {
        [Fact]
        public void When_ProcessDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Step);

            var sut = new RemoveStepFromProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new RemoveStepFromProcessCommand
                {
                    ProcessName = Guid.NewGuid().ToString(),
                    StepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public void When_StepDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Step("test"));

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Process);

            var sut = new RemoveStepFromProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new RemoveStepFromProcessCommand
                {
                    ProcessName = Guid.NewGuid().ToString(),
                    StepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_ProcessContainsStep_Expect_StepCountDecreases()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            process.AddStep(step);

            var originalStepsCount = process.Steps.Count();
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(step);

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(process);

            var sut = new RemoveStepFromProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act
            await sut.Handle(new RemoveStepFromProcessCommand
            {
                ProcessName = process.Id,
                StepId = step.Id
            }, CancellationToken.None);
            
            //Assert
            process.Steps.Count().Should().Be(originalStepsCount - 1);
        }
        
        [Fact]
        public async Task When_ProcessNotContainsStep_Expect_StepsCountDoesNotChange()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            var originalStepsCount = process.Steps.Count();
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(step);

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(process);

            var sut = new RemoveStepFromProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act
            await sut.Handle(new RemoveStepFromProcessCommand
            {
                ProcessName = process.Id,
                StepId = step.Id
            }, CancellationToken.None);
            
            //Assert
            process.Steps.Count().Should().Be(originalStepsCount);
        }
    }
}