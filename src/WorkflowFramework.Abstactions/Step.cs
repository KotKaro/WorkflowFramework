using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkflowFramework.Abstractions
{
    public class Step
    {
        private readonly string _name;
        private readonly IList<StepNavigator> _stepNavigators;

        public IEnumerable<StepNavigator> StepNavigators
        {
            get => _stepNavigators.AsEnumerable();
        }

        public Step(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            _name = name;
            _stepNavigators = new List<StepNavigator>();
        }

        public void AddStepNavigator(StepNavigator stepNavigator)
        {
            if (stepNavigator is null)
            {
                throw new ArgumentNullException(nameof(stepNavigator));
            }

            _stepNavigators.Add(stepNavigator);
        }
    }
}
