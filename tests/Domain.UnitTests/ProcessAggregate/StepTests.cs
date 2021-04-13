using System;
using System.Linq;
using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class StepTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_StepInstanceCreatedWithInvalidName_Expect_ArgumentExceptionThrown(string stepName)
        {
            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Step(new Name(stepName));
            });
        }

        [Fact]
        public void When_StepNavigatorToAddIsNull_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange 
            var step = TestDataFactory.CreateStep();

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                step.AddStepNavigators(new StepNavigator[] { null! });
            });
        }

        [Fact]
        public void When_ValidStepNavigatorAddedToNewStep_Expect_OneStepNavigatorInStep()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();

            //Act
            step.AddStepNavigators(new StepNavigator(TestDataFactory.CreateStep()));

            //Assert
            step.StepNavigators.Count().Should().Be(1);
        }

        [Fact]
        public void When_StepCreatedWithNullStepNavigators_ExpectArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Step(TestDataFactory.CreateStepName(), null);
            });
        }

        [Fact]
        public void When_StepCreatedWithEmptyStepNavigatorArray_Expect_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Step(TestDataFactory.CreateStepName(), Array.Empty<StepNavigator>());
            });
        }

        [Fact]
        public void When_StepAlreadyContainsStepNavigatorWithTargetStep_Expect_StepNavigatorCountDoesNotChange()
        {
            //Arrange
            var step = new Step("test");
            var targetStep = new Step("targetStep");

            //Act
            step.AddStepNavigators(new StepNavigator(targetStep));
            step.AddStepNavigators(new StepNavigator(targetStep));
            
            //Assert
            step.StepNavigators.Count().Should().Be(1);
        }
    }
}
