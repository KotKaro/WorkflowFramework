using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddStepToProcess;
using Common.Tests;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddStepToProcess
{
    public class AddStepToProcessHandlerTests
    {
        [Fact]
        public void When_ProcessDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Step);

            var sut = new AddStepToProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddStepToProcessCommand
                {
                    ProcessName = "test",
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

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Process);

            var sut = new AddStepToProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddStepToProcessCommand
                {
                    ProcessName = "test",
                    StepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_ProcessAlreadyContainsStep_Expect_StepCountDoesNotChange()
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

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(process);
            
            processRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>()))
                .ReturnsAsync(process);

            var sut = new AddStepToProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act
            await sut.Handle(new AddStepToProcessCommand
            {
                ProcessName = process.Name.Value,
                StepId = step.Id
            }, CancellationToken.None);
            
            //Assert
            process.Steps.Count().Should().Be(originalStepsCount);
        }
        
        [Fact]
        public async Task When_ProcessNotContainsStep_Expect_StepShouldBePresentInProcessSteps()
        {
            //Arrange
            var processRepositoryMock = new Mock<IProcessRepository>();
            var stepRepositoryMock = new Mock<IStepRepository>();

            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            stepRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(step);

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(process);
            processRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>()))
                .ReturnsAsync(process);

            var sut = new AddStepToProcessCommandHandler(
                stepRepositoryMock.Object,
                processRepositoryMock.Object
            );

            //Act
            await sut.Handle(new AddStepToProcessCommand
            {
                ProcessName = process.Name.Value,
                StepId = step.Id
            }, CancellationToken.None);
            
            //Assert
            process.Steps.Contains(step).Should().BeTrue();
        }
    }
}