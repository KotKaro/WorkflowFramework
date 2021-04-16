using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.UnitTests.DataFactories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ProcessRunTests
    {
        public static IEnumerable<object> When_ProcessRunInitializedWithoutCorrectArguments_Expect_ArgumentNullExceptionThrown_TCS()
        {
            yield return new object[] { null!, TestDataFactory.CreateStep() };
            yield return new object[] { TestDataFactory.CreateProcess(), null! };
        }

        [Theory]
        [MemberData(nameof(When_ProcessRunInitializedWithoutCorrectArguments_Expect_ArgumentNullExceptionThrown_TCS))]
        public void When_ProcessRunInitializedWithoutCorrectArguments_Expect_ArgumentNullExceptionThrown(Process process, Step startStep)
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ProcessRun(process, startStep);
            });
        }

        [Fact]
        public void When_ProcessDoesNotContainStartStep_Expect_ArgumentExceptionThrown()
        {
            //Arrange
            var process = new Process("test");
            var step = new Step("step");

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ProcessRun(process, step);
            });
        }

        [Fact]
        public void When_ExpectationsNotMet_Expect_ExpectationsNotMetExceptionThrown()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var stepTwo = TestDataFactory.CreateStep();
            var stepThree = TestDataFactory.CreateStep();

            var stepNavigator = new StepNavigator(stepTwo);
            stepNavigator.AddExpectations(new FalseExpectation(typeof(TestClass)));
            
            step.AddStepNavigators(stepNavigator);
            stepTwo.AddStepNavigators(new StepNavigator(stepThree));

            var process = TestDataFactory.CreateProcess(steps: new[] { step, stepTwo, stepThree });
            var processRun = new ProcessRun(process, step);
            
            var repositoryMock = new Mock<IRepository>();
            var factoryMock = new Mock<IRepositoryFactory>();

            repositoryMock.Setup(x => x.GetAll())
                .Returns(new object[] {"test", "test"});
            
            factoryMock.Setup(x => x.GetByEntityType(It.IsAny<Type>()))
                .Returns(repositoryMock.Object);
                
            var specificationResolverService = SpecificationResolverServiceFactory.Create(factoryMock.Object);

            //Act + Assert
            Assert.Throws<ExpectationsNotMetException>(() =>
            {
                processRun.Move(stepTwo, specificationResolverService);
            });
        }

        [Fact]
        public void When_ExpectationsMet_Expect_CurrentStepEqualToCurrentStep()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var stepTwo = TestDataFactory.CreateStep();
            var stepThree = TestDataFactory.CreateStep();

            var stepNavigator = new StepNavigator(stepTwo);
            stepNavigator.AddExpectations(new TrueExpectation(typeof(TestClass)));

            step.AddStepNavigators(stepNavigator);
            stepTwo.AddStepNavigators(new StepNavigator(stepThree));

            var process = TestDataFactory.CreateProcess(steps: new[] { step, stepTwo, stepThree });
            var processRun = new ProcessRun(process, step);

            var repositoryMock = new Mock<IRepository>();
            var factoryMock = new Mock<IRepositoryFactory>();

            repositoryMock.Setup(x => x.GetAll())
                .Returns(new object[] {"test", "test"});
            
            factoryMock.Setup(x => x.GetByEntityType(It.IsAny<Type>()))
                .Returns(repositoryMock.Object);
                
            var specificationResolverService = SpecificationResolverServiceFactory.Create(factoryMock.Object);

            //Act
            processRun.Move(stepTwo, specificationResolverService);

            //Assert
            processRun.CurrentStep.Should().Be(stepTwo);
        }
        
        [Fact]
        public void When_ExpectationsMet_Expect_CanMoveReturnsTrue()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var stepTwo = TestDataFactory.CreateStep();
            var stepThree = TestDataFactory.CreateStep();

            var stepNavigator = new StepNavigator(stepTwo);
            stepNavigator.AddExpectations(new TrueExpectation(typeof(TestClass)));

            step.AddStepNavigators(stepNavigator);
            stepTwo.AddStepNavigators(new StepNavigator(stepThree));

            var process = TestDataFactory.CreateProcess(steps: new[] { step, stepTwo, stepThree });
            var processRun = new ProcessRun(process, step);

            var repositoryMock = new Mock<IRepository>();
            var factoryMock = new Mock<IRepositoryFactory>();

            repositoryMock.Setup(x => x.GetAll())
                .Returns(new object[] {"test", "test"});
            
            factoryMock.Setup(x => x.GetByEntityType(It.IsAny<Type>()))
                .Returns(repositoryMock.Object);
                
            var specificationResolverService = SpecificationResolverServiceFactory.Create(factoryMock.Object);

            //Act
            var result = processRun.CanMove(stepTwo, specificationResolverService);

            //Assert
            result.Should().Be(true);
        }
    }
}
