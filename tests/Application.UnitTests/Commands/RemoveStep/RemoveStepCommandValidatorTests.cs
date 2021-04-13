using System;
using Application.Commands.RemoveStep;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStep
{
    public class RemoveStepCommandValidatorTests
    {
        [Fact]
        public void When_StepIdIsEmpty_Expect_ValidationFail()
        {
            //Arrange
            var sut = new RemoveStepCommandValidator();

            //Act
            var result = sut.Validate(new RemoveStepCommand
            {
                StepId = Guid.Empty
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void When_CommandIsValid_Expect_ValidationNotFail()
        {
            //Arrange
            var sut = new RemoveStepCommandValidator();

            //Act
            var result = sut.Validate(new RemoveStepCommand
            {
                StepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}