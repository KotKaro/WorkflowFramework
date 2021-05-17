using System.Linq;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Expectation.AggregateExpectations
{
    public class OrSpecificationTests
    {
        [Fact]
        public void When_AllSpecificationsAreReturnsFalse_Expect_ResultToBeFalse()
        {
            //Arrange
            var instance = new TestClass
            {
                Name = "test"
            };
            var metadata = new TypeMetadata(typeof(TestClass));

            var namePropertyValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.Name));
            var getHelloMethodValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));
            var getHelloArgument = new Argument(getHelloMethodValueProvider.MethodArguments.ElementAt(0), "test");
            
            var propertyEqualSpecification = new EqualExpectation(namePropertyValueProvider, "false-test");
            var methodEqualSpecification = new EqualExpectation(getHelloMethodValueProvider, "Hello false-test");
            var orSpecification = new OrExpectation(new []
            {
                propertyEqualSpecification, methodEqualSpecification
            });
            
            //Act
            var result = orSpecification.Apply(instance, getHelloArgument);

            //Assert
            result.Should().Be(false);
        }
        
        [Fact]
        public void When_AnyOfSpecificationsReturnsTrue_Expect_ResultToBeTrue()
        {
            //Arrange
            var instance = new TestClass
            {
                Name = "test"
            };
            var metadata = new TypeMetadata(typeof(TestClass));

            var namePropertyValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.Name));
            var getHelloMethodValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));
            var getHelloArgument = new Argument(getHelloMethodValueProvider.MethodArguments.ElementAt(0), "test");
            
            var propertyEqualSpecification = new EqualExpectation(namePropertyValueProvider, "test");
            var methodEqualSpecification = new EqualExpectation(getHelloMethodValueProvider, "Hello false-test");
            var orSpecification = new OrExpectation(new []
            {
                propertyEqualSpecification, methodEqualSpecification
            });
            
            //Act
            var result = orSpecification.Apply(instance, getHelloArgument);

            //Assert
            result.Should().Be(true);
        }
    }
}