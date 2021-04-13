using System;
using Application.Commands.AddStepNavigatorToStep;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.AddStepNavigatorToStep
{
    public class AddStepNavigatorToStepCommandValidatorTests
    {
        [Fact]
        public void When_StepIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddStepNavigatorToStepCommandValidator();
            
            //Act
            var result = sut.Validate(new AddStepNavigatorToStepCommand
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
            var sut = new AddStepNavigatorToStepCommandValidator();
            
            //Act
            var result = sut.Validate(new AddStepNavigatorToStepCommand
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
            var sut = new AddStepNavigatorToStepCommandValidator();
            
            //Act
            var result = sut.Validate(new AddStepNavigatorToStepCommand
            {
                StepId = Guid.NewGuid(),
                TargetStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}