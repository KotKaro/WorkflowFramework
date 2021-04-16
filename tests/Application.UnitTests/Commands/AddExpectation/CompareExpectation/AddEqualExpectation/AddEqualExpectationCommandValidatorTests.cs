using System;
using Application.Commands.AddExpectation.CompareExpectation.AddEqualExpectation;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddEqualExpectation
{
    public class AddEqualExpectationCommandValidatorTests
    {
        [Fact]
        public void When_StepNavigatorIdNotProvided_Expect_ValidationFail()
        {
            //Arrange
            var sut = new AddEqualExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddEqualExpectationCommand
            {
                StepNavigatorId = Guid.Empty,
                ValueAccessorId = Guid.NewGuid(),
                Value = "test"
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_ValueAccessorIdNotProvided_Expect_ValidationFail()
        {
            //Arrange
            var sut = new AddEqualExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddEqualExpectationCommand
            {
                StepNavigatorId = Guid.NewGuid(),
                ValueAccessorId = Guid.Empty,
                Value = "test"
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_ValueIsNull_Expect_ValidationFail()
        {
            //Arrange
            var sut = new AddEqualExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddEqualExpectationCommand
            {
                StepNavigatorId = Guid.NewGuid(),
                ValueAccessorId = Guid.NewGuid(),
                Value = null
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
    }
}