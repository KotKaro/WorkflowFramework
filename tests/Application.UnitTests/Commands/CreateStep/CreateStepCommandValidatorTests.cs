using Application.Commands.CreateStep;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.CreateStep
{
    public class CreateStepCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_CommandIsInvalid_Expect_ValidationFail(string processName)
        {
            //Arrange
            var sut = new CreateStepCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.CreateStep.CreateStepCommand
            {
                StepName = processName,
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_CommandIsValid_Expect_ValidationNotFail()
        {
            //Arrange
            var sut = new CreateStepCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.CreateStep.CreateStepCommand
            {
                StepName = "test",
            });

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}