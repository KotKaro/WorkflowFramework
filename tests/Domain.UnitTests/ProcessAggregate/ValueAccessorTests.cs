using System.Linq;
using Domain.Common;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ValueAccessorTests
    {
        [Fact]
        public void When_GetValueCalledForProperty_Expect_PropertyValueToBeReturned()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());

            //Act
            var value = typeMetadata.ValueAccessors.ElementAt(0).GetValue(testClass);

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("test");
        }

        [Fact]
        public void When_GetValueCalledForMethod_Expect_MethodCorrectResultReturned()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());
            var sut = typeMetadata.ValueAccessors.First(x => x.Name == nameof(TestClass.GetHello));
            var methodArguments = sut.MethodArguments;
            
            //Act
            var value = sut
                .GetValue(testClass, new Argument(methodArguments.ElementAt(0), "karol"));

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("Hello karol");
        }

        [Fact]
        public void
            When_GetValueCalledForMethod_Expect_MethodCorrectResultReturnedEventIfArgumentsAreProvidedInIncorrectOrder()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());
            var getHelloValueAccessor = typeMetadata.ValueAccessors.Last(x => x.Name == nameof(TestClass.GetHello));
            var methodArguments = getHelloValueAccessor.MethodArguments;
            var firstMethodArgument = new Argument(methodArguments.ElementAt(0), "karol");
            var secondMethodArgument = new Argument(methodArguments.ElementAt(1), 123);
            
            //Act
            var value = getHelloValueAccessor
                .GetValue(testClass, secondMethodArgument, firstMethodArgument);

            //Assert
            ((string) value).Should().Be("Hello karol 123");
        }

        [Fact]
        public void When_ParameterNotFoundInProvidedInstance_ExpectValueAccessorNotFoundException()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var notExistingValueAccessor = new ValueAccessor("prop", typeof(TestClass));


            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueAccessor.GetValue(testClass); });
        }

        [Fact]
        public void When_MethodNotFoundInProvidedInstance_ExpectValueAccessorNotFoundException()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var notExistingValueAccessor =
                new ValueAccessor("prop", typeof(TestClass), new MemberDescriptor("test"));

            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueAccessor.GetValue(testClass); });
        }

        [Fact]
        public void When_ExtraNotRelatedArgumentsProvidedForGetValue_Expect_ValueReturnedCorrectly()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());
            var getHelloValueAccessor = typeMetadata.ValueAccessors.First(x => x.Name == nameof(TestClass.GetHello));

            //Act
            var methodArguments = getHelloValueAccessor.MethodArguments;

            var value = getHelloValueAccessor
                .GetValue(testClass, new Argument(methodArguments.ElementAt(0), "karol"),
                    new Argument(new MemberDescriptor("no_existing_one"), "karol"));

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("Hello karol");
        }

        [Fact]
        public void When_TriedToGetValueForNullInstance_Expect_NullValueReturned()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());
            var getHelloValueAccessor = typeMetadata.ValueAccessors.First(x => x.Name == nameof(TestClass.Name));

            //Act
            var value = getHelloValueAccessor
                .GetValue(null);

            //Assert
            value.GetType().Should().Be(typeof(NullValue));
        }
    }
}