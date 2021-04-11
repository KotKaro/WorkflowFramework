using System.Linq;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Specification
{
    public class LessThanSpecificationTests
    {
        [Theory]
        [InlineData(10, 9, true)]
        [InlineData(9, 10, false)]
        public void When_LessThanSpecificationAppliedForSpecificValues_Expect_ExpectedResult(
            int numberValue,
            int specificationValue,
            bool expectedResult
        )
        {
            //Arrange
            var instance = new TestClass
            {
                Number = numberValue
            };
            
            var metadata = new TypeMetadata(typeof(TestClass));
            var namePropertyValueAccessor = metadata.ValueAccessors.First(x => x.Name == nameof(TestClass.Number));
            var sut = new LessThanExpectation(namePropertyValueAccessor, specificationValue);

            //Act
            var result = sut.Apply(instance);
  
            //Assert
            result.Should().Be(expectedResult);
        }
        
        [Fact]
        public void When_NotComparableValuesProvided_Expect_CannotCompareExceptionThrown()
        {
            //Arrange
            var instance = new TestClass
            {
                Name = "test_name"
            };
            
            var metadata = new TypeMetadata(typeof(TestClass));
            var namePropertyValueAccessor = metadata.ValueAccessors.First(x => x.Name == nameof(TestClass.Name));
            var sut = new LessThanExpectation(namePropertyValueAccessor, "test");

            //Act + Assert
            Assert.Throws<CannotCompareException>(() =>
            {
                sut.Apply(instance);
            });
        }
    }
}