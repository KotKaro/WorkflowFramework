using System.Linq;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Expectation.CompareExpectations
{
    public class EqualExpectationTests
    {
        [Fact]
        public void When_EqualSpecificationAppliedForEqualPropertyValues_Expect_TrueResult()
        {
            //Arrange
            var instance = new TestClass
            {
                Name = "test"
            };
            var metadata = new TypeMetadata(typeof(TestClass));

            var namePropertyValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.Name));
            
            var equalSpecification = new EqualExpectation(namePropertyValueProvider, "test");

            //Act
            var result = equalSpecification.Apply(instance);

            //Assert
            result.Should().Be(true);
        }
        
        [Fact]
        public void When_EqualSpecificationAppliedForEqualMethodValues_Expect_TrueResult()
        {
            //Arrange
            var instance = new TestClass();
            var metadata = new TypeMetadata(typeof(TestClass));

            var getHelloValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));
            
            var equalSpecification = new EqualExpectation(getHelloValueProvider, "Hello test");

            //Act
            var argument = new Argument(getHelloValueProvider.MethodArguments.ElementAt(0), "test");
            var result = equalSpecification.Apply(instance, argument);

            //Assert
            result.Should().Be(true);
        }
        
        [Fact]
        public void When_EqualSpecificationAppliedForNotEqualPropertyValues_Expect_FalseResult()
        {
            //Arrange
            var instance = new TestClass
            {
                Name = "test"
            };
            var metadata = new TypeMetadata(typeof(TestClass));

            var namePropertyValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.Name));
            
            var equalSpecification = new EqualExpectation(namePropertyValueProvider, "false-test");

            //Act
            var result = equalSpecification.Apply(instance);

            //Assert
            result.Should().Be(false);
        }
    }
}