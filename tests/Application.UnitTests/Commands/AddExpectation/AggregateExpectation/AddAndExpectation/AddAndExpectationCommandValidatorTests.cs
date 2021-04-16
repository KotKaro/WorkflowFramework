using System;
using Application.Commands.AddExpectation.AggregateExpectation.AddAndExpectation;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.AggregateExpectation.AddAndExpectation
{
    public class AddAndExpectationCommandValidatorTests
    {
        [Fact]
        public void When_ExpectationIdsAreNull_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddAndExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddAndExpectationCommand
            {
                ExpectationIds = null,
                StepNavigatorId = Guid.NewGuid()
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void When_ExpectationIdsGotLessThanTwoExpectationIds_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddAndExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddAndExpectationCommand
            {
                ExpectationIds = new[]
                {
                    Guid.NewGuid()
                },
                StepNavigatorId = Guid.NewGuid()
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_StepNavigatorIdIsEmpty_Expect_ValidationFails()
        {
            //Arrange
            var sut = new AddAndExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddAndExpectationCommand
            {
                ExpectationIds = new[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                StepNavigatorId = Guid.Empty
            });

            //Assert
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void When_CommandIsCorrect_Expect_ValidationResultIsValid()
        {
            //Arrange
            var sut = new AddAndExpectationCommandValidator();

            //Act
            var result = sut.Validate(new AddAndExpectationCommand
            {
                ExpectationIds = new[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                StepNavigatorId = Guid.NewGuid()
            });

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}