using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Tests;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Domain.Services;
using Domain.UnitTests.DataFactories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ProcessTests
    {
        private readonly ExpectationResolverService _expectationResolverService;
        private readonly IProcessRepository _processRepository;

        public ProcessTests()
        {
            _expectationResolverService = SpecificationResolverServiceFactory.Create();
            var mock = new Mock<IProcessRepository>();
            mock.Setup(x => x.GetByName(It.IsAny<String>()))
                .Returns(Task.FromResult(null as Process));
            
            _processRepository = mock.Object;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_ProcessCreateWithoutName_Expect_ArgumentNullExceptionThrown(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                TestDataFactory.CreateProcess(name, new[] {TestDataFactory.CreateStep()});
            });
        }

        public static IEnumerable<object[]>
            When_ProcessIntendedToCreateWithoutAnyStep_Expect_ArgumentExceptionThrown_TestCaseSource()
        {
            yield return new object[] {null!};
            yield return new object[] {Array.Empty<Step>()};
        }

        [Theory]
        [MemberData(nameof(When_ProcessIntendedToCreateWithoutAnyStep_Expect_ArgumentExceptionThrown_TestCaseSource))]
        public void When_ProcessIntendedToCreateWithoutAnyStep_Expect_ArgumentExceptionThrown(Step[] steps)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                Process.Create("test", steps, _processRepository);
            });
        }


        [Fact]
        public void When_OriginStepDoesNotExistsInProcess_Expect_StepNotInProcessException()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = TestDataFactory.CreateProcess("test", new[] {step});

            //Act + Assert
            Assert.Throws<StepNotInProcessException>(() =>
            {
                process.CanMove(TestDataFactory.CreateStep(), step, _expectationResolverService);
            });
        }

        [Fact]
        public void When_TargetStepDoesNotExistsInProcess_Expect_StepNotInProcessException()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = TestDataFactory.CreateProcess("test", new[] {step});

            //Act + Assert
            Assert.Throws<StepNotInProcessException>(() =>
            {
                process.CanMove(step, TestDataFactory.CreateStep(), _expectationResolverService);
            });
        }

        [Fact]
        public void When_MoveCalledButNoStepNavigatorFromCurrentStepToTarget_Expect_StepNavigatorNotFoundException()
        {
            //Arrange
            var originStep = TestDataFactory.CreateStep("step1");
            var targetStep = TestDataFactory.CreateStep("step2");

            var process = TestDataFactory.CreateProcess("test", new[] {originStep, targetStep});

            //Act + Assert
            Assert.Throws<StepNavigatorNotFoundException>(() =>
            {
                process.CanMove(originStep, targetStep, _expectationResolverService);
            });
        }

        [Fact]
        public void When_OriginStepNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = TestDataFactory.CreateProcess("test", new[] {step});

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => { process.CanMove(null, step, _expectationResolverService); });
        }

        [Fact]
        public void When_TargetStepNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = TestDataFactory.CreateProcess("test", new[] {step});

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                process.CanMove(
                    TestDataFactory.CreateStep(),
                    null,
                    _expectationResolverService
                );
            });
        }

        [Fact]
        public void When_StepNotProvidedForAddStep_Expect_ArgumentNullException()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test", new List<Step>
            {
                new("test")
            });

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => { process.AddStep(null); });
        }

        [Fact]
        public void When_StepProvidedForAddStep_Expect_NewStepPresentInProcessSteps()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test", new List<Step>
            {
                new("test")
            });

            //Act
            process.AddStep(new Step("SomeStep"));

            //Assert
            process.Steps.Should().Contain(x => x.Name.Value == "SomeStep");
        }
        
        [Fact]
        public void When_TryingRemoveStepFromProcessAndStepIsNull_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test", new List<Step>
            {
                new("test")
            });

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                process.RemoveStep(null);
            });
        }
        
        [Fact]
        public void When_TryingRemoveStepFromProcessAndStepNotInProcess_Expect_StepsCountDoesNotChange()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            process.AddStep(step);

            //Act
            process.RemoveStep(new Step("test1"));
            
            //Assert
            process.Steps.Count().Should().Be(1);
        }
        
        [Fact]
        public void When_TryingRemoveStepFromProcessAndStepInProcess_Expect_StepRemovedFromProcess()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            process.AddStep(step);

            //Act
            process.RemoveStep(step);
            
            //Assert
            process.Steps.Count().Should().Be(0);
        }

        [Fact]
        public void When_GotStepCalledAndStepInProcess_Expect_ResultIsTrue()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");
            
            process.AddStep(step);

            //Act
            var result = process.GotStep(step);
            
            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public void When_GotStepCalledAndStepNotInProcess_Expect_ResultIsFalse()
        {
            //Arrange
            var process = TestDataFactory.CreateProcess("test");
            var step = new Step("test");

            //Act
            var result = process.GotStep(step);
            
            //Assert
            result.Should().BeFalse();
        }
    }
}