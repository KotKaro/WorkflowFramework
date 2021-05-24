using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.ProcessAggregate
{
    public class  Step : GuidEntity
    {
        private readonly ICollection<StepNavigator> _stepNavigators;
        
        public Name Name { get; }
        public IEnumerable<StepNavigator> StepNavigators => _stepNavigators.AsEnumerable();

        private Step()
        {
            _stepNavigators ??= new List<StepNavigator>();
        }
        
        public Step(Name name) : this()
        {
            Name = name ?? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
        }

        public Step(Name name, params StepNavigator[] stepNavigators) : this(name)
        {
            AddStepNavigators(stepNavigators);
        }

        public void AddStepNavigators(params StepNavigator[] stepNavigators)
        {
            if (!stepNavigators?.Any() ?? true)
            {
                throw new ArgumentException("Intended to add step navigators but provided collection is null or empty!");
            }

            foreach (var stepNavigator in stepNavigators)
            {
                if (stepNavigator is null)
                {
                    throw new ArgumentException(nameof(stepNavigator));
                }

                if (GotStepNavigatorWithTargetStepId(stepNavigator.TargetStep.Id))
                {
                    continue;
                }

                _stepNavigators.Add(stepNavigator);
            }
        }

        public bool GotStepNavigatorWithTargetStepId(Guid id)
        {
            return _stepNavigators.Any(x => x.TargetStep.Id == id);
        }

        public void RemoveStepNavigator(StepNavigator stepNavigator)
        {
            var stepNavigatorFromList = _stepNavigators.FirstOrDefault(x => x.Equals(stepNavigator));

            if (stepNavigatorFromList != null)
            {
                _stepNavigators.Remove(stepNavigatorFromList);
            }
        }
    }
}
