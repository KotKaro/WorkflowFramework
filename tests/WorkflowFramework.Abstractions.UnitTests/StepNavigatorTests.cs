using System;
using System.Linq;
using NUnit.Framework;

namespace WorkflowFramework.Abstractions.UnitTests
{
    [TestFixture]
    public class StepNavigatorTests
    {
        [Test]
        public void When_StepNavigatorCreatedWithoutTargetStep_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StepNavigator(null, new Expectation());
            });

            //Assert
        }
    }
}
