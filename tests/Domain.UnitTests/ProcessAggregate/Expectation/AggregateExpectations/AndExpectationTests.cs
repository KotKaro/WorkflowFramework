using System;
using System.Linq;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Expectation.AggregateExpectations
{
    public class AndExpectationTests
    {
        [Fact]
        public void When_AllSpecificationsAreReturnsTrue_Expect_ResultToBeTrue()
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
            var methodEqualSpecification = new EqualExpectation(getHelloMethodValueProvider, "Hello test");
            var andSpecification = new AndExpectation(new []
            {
                propertyEqualSpecification, methodEqualSpecification
            });
            
            //Act
            var result = andSpecification.Apply(instance, getHelloArgument);

            //Assert
            result.Should().Be(true);
        }
        
        [Fact]
        public void When_AnyOfSpecificationsReturnsFalse_Expect_ResultToBeFalse()
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
            var andSpecification = new AndExpectation(new []
            {
                propertyEqualSpecification, methodEqualSpecification
            });
            
            //Act
            var result = andSpecification.Apply(instance, getHelloArgument);

            //Assert
            result.Should().Be(false);
        }
        
        [Fact]
        public void When_TriedToCreateAggregateSpecificationWithDifferentSpecifications_Expect_AmbiguousSpecificationsTypesException()
        {
            //Arrange
            var metadata = new TypeMetadata(typeof(TestClass));

            var differentTypeValueProvider = new ValueProvider("wrong", typeof(Type), typeof(Type));
            var getHelloMethodValueProvider = metadata.ValueProviders.First(x => x.Name == nameof(TestClass.GetHello));
            
            var propertyEqualSpecification = new EqualExpectation(differentTypeValueProvider, "test");
            var methodEqualSpecification = new EqualExpectation(getHelloMethodValueProvider, "Hello false-test");
            
            
            //Act + Assert
            Assert.Throws<AmbiguousSpecificationsTypesException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new AndExpectation(new[]
                {
                    propertyEqualSpecification, methodEqualSpecification
                });
            });
        }
    }
}