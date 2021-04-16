using System;
using Application.Commands.RemoveExpectation;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.RemoveExpectation
{
    public class RemoveExpectationCommandValidatorTests
    {
        [Fact]
        public void When_ExpectationIdIsEmpty_Expect_ValidationFail()
        {
            //Arrange
            var sut = new RemoveExpectationCommandValidator();
            
            //Act
            var result = sut.Validate(new RemoveExpectationCommand
            {
                ExpectationId = Guid.Empty
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void When_CommandIsValid_Expect_ValidationIsSuccess()
        {
            //Arrange
            var sut = new RemoveExpectationCommandValidator();
            
            //Act
            var result = sut.Validate(new RemoveExpectationCommand
            {
                ExpectationId = Guid.NewGuid()
            });

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}