using Domain.ProcessAggregate.Expectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Expectation
{
    public class NegationExpectationTests
    {
        [Fact]
        public void When_ExpectationProvided_Expect_ReturnedValueToBeNegated()
        {
            //Arrange
            var trueExpectation = new TrueExpectation(GetType());
            var negationExpectation = new ExpectationNegation(trueExpectation);

            //Act
            var originalResult = trueExpectation.Apply(null!);
            var result = negationExpectation.Apply(null);

            //Assert
            result.Should().BeFalse();
            originalResult.Should().BeTrue();
        }
    }
}