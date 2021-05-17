using System.Linq;
using Domain.Common;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ValueProviderTests
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
            var value = typeMetadata.ValueProviders.ElementAt(0).GetValue(testClass);

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
            var sut = typeMetadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));
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
            var getHelloValueProvider = typeMetadata.ValueProviders.Last(x => x.Name == nameof(TestClass.GetHello));
            var methodArguments = getHelloValueProvider.MethodArguments;
            var firstMethodArgument = new Argument(methodArguments.ElementAt(0), "karol");
            var secondMethodArgument = new Argument(methodArguments.ElementAt(1), 123);
            
            //Act
            var value = getHelloValueProvider
                .GetValue(testClass, secondMethodArgument, firstMethodArgument);

            //Assert
            ((string) value).Should().Be("Hello karol 123");
        }

        [Fact]
        public void When_ParameterNotFoundInProvidedInstance_ExpectValueProviderNotFoundException()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var notExistingValueProvider = new ValueProvider("prop", typeof(TestClass), typeof(TestClass));

            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueProvider.GetValue(testClass); });
        }

        [Fact]
        public void When_MethodNotFoundInProvidedInstance_ExpectValueProviderNotFoundException()
        {
            //Arrange
            var testClass = new TestClass
            {
                Name = "test"
            };

            var notExistingValueProvider = new ValueProvider(
                "prop",
                typeof(TestClass),
                typeof(TestClass),
                new MemberDescriptor("test", GetType())
            );

            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueProvider.GetValue(testClass); });
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
            var getHelloValueProviders = typeMetadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));

            //Act
            var methodArguments = getHelloValueProviders.MethodArguments;

            var value = getHelloValueProviders
                .GetValue(testClass, new Argument(methodArguments.ElementAt(0), "karol"),
                    new Argument(new MemberDescriptor("no_existing_one", typeof(string)), "karol"));

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
            var getHelloValueProvider = typeMetadata.ValueProviders.First(x => x.Name == nameof(TestClass.Name));

            //Act
            var value = getHelloValueProvider
                .GetValue(null);

            //Assert
            value.GetType().Should().Be(typeof(NullValue));
        }
    }
}