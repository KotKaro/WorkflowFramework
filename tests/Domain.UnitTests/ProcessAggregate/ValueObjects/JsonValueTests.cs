using System;
using Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.ValueObjects
{
    public class JsonValueTests
    {
        [Fact]
        public void When_ValueIsNull_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new JsonValue(null);
            });
        }
        
        [Fact]
        public void When_ValueIsCorrect_Expect_OriginalValueGotSameType()
        {
            //Arrange
            var jsonValue = new JsonValue("test");
            
            //Act
            var originalValue = jsonValue.GetOriginalValue();
            
            //Assert
            originalValue.GetType().Should().Be(typeof(string));
        }
    }
}