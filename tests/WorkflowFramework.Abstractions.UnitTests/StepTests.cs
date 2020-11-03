using System;
using System.Linq;
using NUnit.Framework;

namespace WorkflowFramework.Abstractions.UnitTests
{
    [TestFixture]
    public class StepTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void When_StepInstanceCreatedWithInvalidName_Expect_ArgumentExceptionThrown(string stepName)
        {
            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var step = new Step(stepName);
            });
        }

        [Test]
        public void When_StepNavigatorToAddIsNull_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange 
            var step = new Step("test");

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                step.AddStepNavigator(null);
            });
        }

        [Test]
        public void When_ValidStepNavigatorAddedToNewStep_Expect_OneStepNavigatorInStep()
        {
            //Arrange
            var step = new Step("test");

            //Act
            step.AddStepNavigator(new StepNavigator(new Step("test1")));

            //Assert
            Assert.That(step.StepNavigators.Count(), Is.EqualTo(1));

        }
    }
}
