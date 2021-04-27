using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateProcessRun;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.CreateProcessRun
{
    public class CreateProcessRunCommandHandlerTests
    {
        [Fact]
        public async Task When_ProcessNotFound_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var sut = CreateHandler(out _, out _, out _);

            //Act + Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new CreateProcessRunCommand
                {
                    ProcessId = Guid.NewGuid(),
                    StartStepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task When_StartStepNotFoundInProcess_Expect_StepNotInProcessExceptionThrown()
        {
            //Arrange
            var sut = CreateHandler(out var processRepositoryMock, out _, out _);

            processRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Process("test"));

            //Act + Assert
            await Assert.ThrowsAsync<StepNotInProcessException>(async () =>
            {
                await sut.Handle(new CreateProcessRunCommand
                {
                    ProcessId = Guid.NewGuid(),
                    StartStepId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task When_MemberDescriptorWithProvidedIdDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var process = new Process("test");
            var step = new Step("test");

            process.AddStep(step);

            var sut = CreateHandler(out var processRepositoryMock, out _ , out _);

            processRepositoryMock.Setup(x => x.GetByIdAsync(process.Id))
                .ReturnsAsync(process);

            //Act
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new CreateProcessRunCommand
                {
                    ProcessId = process.Id,
                    StartStepId = step.Id,
                    ArgumentDTOs = new[]
                    {
                        new ArgumentDto
                        {
                            Value = null,
                            MemberDescriptorId = Guid.NewGuid()
                        }
                    }
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task When_ProcessAndStepExists_Expect_ProcessRunAdded()
        {
            //Arrange
            var process = new Process("test");
            var step = new Step("test");

            process.AddStep(step);

            var sut = CreateHandler(out var processRepositoryMock, out var processRunRepositoryMock, out _);

            processRepositoryMock.Setup(x => x.GetByIdAsync(process.Id))
                .ReturnsAsync(process);

            //Act
            await sut.Handle(new CreateProcessRunCommand
            {
                ProcessId = process.Id,
                StartStepId = step.Id
            }, CancellationToken.None);

            //Assert
            processRunRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<ProcessRun>()), Times.Once);
        }

        private CreateProcessRunCommandHandler CreateHandler(
            out Mock<IProcessRepository> processRepositoryMock,
            out Mock<IProcessRunRepository> processRunRepositoryMock,
            out Mock<IMemberDescriptorRepository> memberDescriptorRepository
        )
        {
            processRepositoryMock = new Mock<IProcessRepository>();
            processRunRepositoryMock = new Mock<IProcessRunRepository>();
            memberDescriptorRepository = new Mock<IMemberDescriptorRepository>();

            return new CreateProcessRunCommandHandler(
                processRepositoryMock.Object,
                processRunRepositoryMock.Object,
                memberDescriptorRepository.Object
            );
        }
    }
}