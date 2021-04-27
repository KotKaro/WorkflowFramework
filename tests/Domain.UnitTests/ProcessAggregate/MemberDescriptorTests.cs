using System;
using Domain.ProcessAggregate;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class MemberDescriptorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_NameIsNullOrEmpty_Expect_ArgumentExceptionThrown(string name)
        {
            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MemberDescriptor(name, GetType());
            });
        }
        
        [Fact]
        public void When_TypeNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MemberDescriptor("name", null);
            });
        }
    }
}