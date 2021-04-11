using System;
using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Services;
using Domain.UnitTests.DataFactories;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class ProcessTests
    {
        private readonly ExpectationResolverService _expectationResolverService;

        public ProcessTests()
        {
            _expectationResolverService = SpecificationResolverServiceFactory.Create();
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
                new Process(new Name(name), new[] {TestDataFactory.CreateStep()});
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
                new Process(new Name("test"), steps);
            });
        }


        [Fact]
        public void When_OriginStepDoesNotExistsInProcess_Expect_StepNotInProcessException()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = new Process(new Name("test"), new[] {step});

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
            var process = new Process(new Name("test"), new[] {step});

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

            var process = new Process(new Name("test"), new[] {originStep, targetStep});

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
            var process = new Process(new Name("test"), new[] {step});

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => { process.CanMove(null, step, _expectationResolverService); });
        }

        [Fact]
        public void When_TargetStepNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Arrange
            var step = TestDataFactory.CreateStep();
            var process = new Process(new Name("test"), new[] {step});

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
            var process = new Process("test", new List<Step>
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
            var process = new Process("test", new List<Step>
            {
                new("test")
            });

            //Act
            process.AddStep(new Step("SomeStep"));

            //Assert
            process.Steps.Should().Contain(x => x.Name.Value == "SomeStep");
        }
    }
}