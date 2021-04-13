using Application.Commands.CreateProcess;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.CreateProcess
{
    public class CreateProcessCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_CommandIsInvalid_Expect_ValidationFail(string processName)
        {
            //Arrange
            var sut = new CreateProcessCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.CreateProcess.CreateProcessCommand
            {
                ProcessName = processName,
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void When_CommandIsValid_Expect_ValidationNotFail()
        {
            //Arrange
            var sut = new CreateProcessCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.CreateProcess.CreateProcessCommand
            {
                ProcessName = "test",
            });

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}