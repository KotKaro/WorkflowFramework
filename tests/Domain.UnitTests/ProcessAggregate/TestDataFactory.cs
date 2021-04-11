using System.Collections.Generic;
using System.Linq;
using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;

namespace Domain.UnitTests.ProcessAggregate
{
    public class TestDataFactory
    {
        public static Process CreateProcess(string name = "some process", IEnumerable<Step> steps = null!)
        {
            var stepsArray = (steps ?? Enumerable.Range(0, 10).Select(x => CreateStep())).ToArray();
            return new Process(
                new Name(name),
                stepsArray
            );
        }

        public static Step CreateStep(string stepName = "stepName")
        {
            return new(CreateStepName(stepName));
        }

        public static Name CreateStepName(string name = "step name")
        {
            return new Name(name);
        }
    }
}
