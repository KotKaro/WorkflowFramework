using System;
using Application.Commands.AddStepToProcess;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.AddStepToProcess
{
    public class AddStepToProcessValidatorTests
    {
        [Fact]
        public void When_ProcessIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddStepToProcessValidator();
            
            //Act
            var result = sut.Validate(new AddStepToProcessCommand
            {
                ProcessName = "",
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
                ProcessName = "test",
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
                ProcessName = "test",
                StepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}