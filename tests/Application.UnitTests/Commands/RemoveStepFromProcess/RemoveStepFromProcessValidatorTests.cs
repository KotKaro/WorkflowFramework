using System;
using Application.Commands.AddStepToProcess;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.RemoveStepFromProcess
{
    public class RemoveStepFromProcessValidatorTests
    {
        [Fact]
        public void When_ProcessIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddStepToProcessValidator();
            
            //Act
            var result = sut.Validate(new AddStepToProcessCommand
            {
                ProcessName = string.Empty,
                StepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_StepIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddStepToProcessValidator();
            
            //Act
            var result = sut.Validate(new AddStepToProcessCommand
            {
                ProcessName = Guid.NewGuid().ToString(),
                StepId = Guid.Empty
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_CommandIsValid_Expect_ValidationIsValid()
        {
            //Arrange
            var sut = new AddStepToProcessValidator();
            
            //Act
            var result = sut.Validate(new AddStepToProcessCommand
            {
                ProcessName = Guid.NewGuid().ToString(),
                StepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}