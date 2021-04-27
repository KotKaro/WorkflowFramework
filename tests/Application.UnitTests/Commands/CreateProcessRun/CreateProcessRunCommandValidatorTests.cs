using System;
using Application.Commands.CreateProcessRun;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.CreateProcessRun
{
    public class CreateProcessRunCommandValidatorTests
    {
        [Fact]
        public void When_ProcessIdDoesNotProvided_Expect_ValidationFail()
        {
            //Arrange
            var sut = new CreateProcessRunCommandValidator();
            
            //Act
            var result = sut.Validate(new CreateProcessRunCommand
            {
                ProcessId = Guid.Empty,
                ArgumentDTOs = Array.Empty<ArgumentDto>(),
                StartStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_StartStepIdDoesNotProvided_Expect_ValidationFail()
        {
            //Arrange
            var sut = new CreateProcessRunCommandValidator();
            
            //Act
            var result = sut.Validate(new CreateProcessRunCommand
            {
                ProcessId = Guid.NewGuid(),
                ArgumentDTOs = Array.Empty<ArgumentDto>(),
                StartStepId = Guid.Empty
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_AnyArgumentProvidedButDoesNotHaveMemberDescriptorId_Expect_ValidationFail()
        {
            //Arrange
            var sut = new CreateProcessRunCommandValidator();
            
            //Act
            var result = sut.Validate(new CreateProcessRunCommand
            {
                ProcessId = Guid.NewGuid(),
                ArgumentDTOs = new ArgumentDto[]
                {
                  new()
                  {
                      Value = null,
                      MemberDescriptorId = Guid.Empty
                  }  
                },
                StartStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_CorrectCommandProvided_Expect_ValidationPass()
        {
            //Arrange
            var sut = new CreateProcessRunCommandValidator();
            
            //Act
            var result = sut.Validate(new CreateProcessRunCommand
            {
                ProcessId = Guid.NewGuid(),
                ArgumentDTOs = new ArgumentDto[]
                {
                    new()
                    {
                        Value = null,
                        MemberDescriptorId = Guid.NewGuid()
                    }  
                },
                StartStepId = Guid.NewGuid()
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}