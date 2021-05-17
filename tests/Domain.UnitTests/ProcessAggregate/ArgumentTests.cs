using System;
using Domain.ProcessAggregate;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ArgumentTests
    {
        [Fact]
        public void When_ProvidedValueCannotBeDeserializedUsingMemberDescriptorType_Expect_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Argument(new MemberDescriptor("name", typeof(string)), new TestClass
                {
                    Name = "test",
                    Number = 1
                });
            });
        }
    }
}