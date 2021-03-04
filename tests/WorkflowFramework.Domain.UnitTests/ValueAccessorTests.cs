using FluentAssertions;
using NUnit.Framework;
using WorkflowFramework.Domain.Exceptions;
using WorkflowFramework.Domain.ProcessAggregate;

namespace WorkflowFramework.Domain.UnitTests
{
    [TestFixture]
    public class ValueAccessorTests
    {
        [Test]
        public void When_GetValueCalledForProperty_Expect_PropertyValueToBeReturned()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());

            //Act
            var value = typeMetadata.ValueAccessors[0].GetValue(testClass);

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("test");
        }

        [Test]
        public void When_GetValueCalledForMethod_Expect_MethodCorrectResultReturned()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());

            //Act
            var methodArguments = typeMetadata.ValueAccessors[1].MethodArguments;

            var value = typeMetadata.ValueAccessors[1]
                .GetValue(testClass, new Argument(methodArguments[0], "karol"));

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("Hello karol");
        }

        [Test]
        public void
            When_GetValueCalledForMethod_Expect_MethodCorrectResultReturnedEventIfArgumentsAreProvidedInIncorrectOrder()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());

            //Act
            var methodArguments = typeMetadata.ValueAccessors[2].MethodArguments;
            var firstMethodArgument = new Argument(methodArguments[0], "karol");
            var secondMethodArgument = new Argument(methodArguments[1], 123);

            var value = typeMetadata.ValueAccessors[2].GetValue(testClass, secondMethodArgument, firstMethodArgument);

            //Assert
            ((string) value).Should().Be("Hello karol 123");
        }

        [Test]
        public void When_ParameterNotFoundInProvidedInstance_ExpectValueAccessorNotFoundException()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var notExistingValueAccessor = new ValueAccessor("prop", typeof(TestClass));


            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueAccessor.GetValue(testClass); });
        }

        [Test]
        public void When_MethodNotFoundInProvidedInstance_ExpectValueAccessorNotFoundException()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var notExistingValueAccessor =
                new ValueAccessor("prop", typeof(TestClass), new MemberDescriptor("test", typeof(TestClass)));

            //Act + Assert
            Assert.Throws<ValueNotReachableException>(() => { notExistingValueAccessor.GetValue(testClass); });
        }

        [Test]
        public void When_ExtraNotRelatedArgumentsProvidedForGetValue_Expect_ValueReturnedCorrectly()
        {
            //Arrange
            var testClass = new TestClass()
            {
                Name = "test"
            };

            var typeMetadata = new TypeMetadata(testClass.GetType());

            //Act
            var methodArguments = typeMetadata.ValueAccessors[1].MethodArguments;

            var value = typeMetadata.ValueAccessors[1]
                .GetValue(testClass, new Argument(methodArguments[0], "karol"),
                    new Argument(new MemberDescriptor("no_existing_one", typeof(TestClass)), "karol"));

            //Assert
            value.GetType().Should().Be<string>();
            ((string) value).Should().Be("Hello karol");
        }
    }
}