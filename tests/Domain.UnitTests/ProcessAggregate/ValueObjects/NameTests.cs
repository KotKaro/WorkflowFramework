using Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void When_NameToString_Expect_CorrectNameValueReturned()
        {
            //Arrange
            var name = new Name("test");

            //Act
            var nameStr = name.ToString();

            //Assert
            nameStr.Should().Be("test");
        }
    }
}