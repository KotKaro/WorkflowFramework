using System.Linq;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate.Specification
{
    public class BiggerThanSpecificationTests
    {
        [Theory]
        [InlineData(10, 9, false)]
        [InlineData(9, 10, true)]
        public void When_BiggerThanSpecificationAppliedForSpecificValues_Expect_ExpectedResult(
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
            var sut = new BiggerThanExpectation(namePropertyValueAccessor, specificationValue);

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
            var sut = new BiggerThanExpectation(namePropertyValueAccessor, "test");

            //Act + Assert
            Assert.Throws<CannotCompareException>(() =>
            {
                sut.Apply(instance);
            });
        }
    }
}