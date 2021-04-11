using System.Linq;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Specification
{
    public class EqualSpecificationTests
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

            var namePropertyValueAccessor = metadata.ValueAccessors.First(x => x.Name == nameof(TestClass.Name));
            
            var equalSpecification = new EqualExpectation(namePropertyValueAccessor, "test");

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

            var getHelloValueAccessor = metadata.ValueAccessors.First(x => x.Name == nameof(TestClass.GetHello));
            
            var equalSpecification = new EqualExpectation(getHelloValueAccessor, "Hello test");

            //Act
            var argument = new Argument(getHelloValueAccessor.MethodArguments.ElementAt(0), "test");
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

            var namePropertyValueAccessor = metadata.ValueAccessors.First(x => x.Name == nameof(TestClass.Name));
            
            var equalSpecification = new EqualExpectation(namePropertyValueAccessor, "false-test");

            //Act
            var result = equalSpecification.Apply(instance);

            //Assert
            result.Should().Be(false);
        }
    }
}