using System;
using Application.Commands.RemoveStepNavigatorFromStep;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStepNavigatorFromStep
{
    public class RemoveStepNavigatorFromStepCommandValidatorTests
    {
        [Fact]
        public void When_StepIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new RemoveStepNavigatorFromStepCommandValidator();
            
            //Act
            var result = sut.Validate(new RemoveStepNavigatorFromStepCommand
            {
                StepId = Guid.Empty,
                TargetStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_TargetStepIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new RemoveStepNavigatorFromStepCommandValidator();
            
            //Act
            var result = sut.Validate(new RemoveStepNavigatorFromStepCommand
            {
                StepId = Guid.NewGuid(),
                TargetStepId = Guid.Empty
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_CommandIsValid_Expect_ValidationIsValid()
        {
            //Arrange
            var sut = new RemoveStepNavigatorFromStepCommandValidator();
            
            //Act
            var result = sut.Validate(new RemoveStepNavigatorFromStepCommand
            {
                StepId = Guid.NewGuid(),
                TargetStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}