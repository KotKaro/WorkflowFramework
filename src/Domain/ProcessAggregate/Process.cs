using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Exceptions;
using Domain.Services;

namespace Domain.ProcessAggregate
{
    public class Process : Entity
    {
        private readonly ICollection<Step> _steps;

        public Name Name { get; }
        public IEnumerable<Step> Steps => _steps.ToArray();

        private Process() { }
        
        public Process(Name name, ICollection<Step> steps)
        {
            if (!steps?.Any() ?? true)
            {
                throw new ArgumentException("Process steps not provided!", nameof(steps));
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));
            _steps = new List<Step>();

            foreach (var step in steps)
            {
                AddStep(step);    
            }
        }
        
        public bool CanMove(Step originStep, Step targetStep, ExpectationResolverService expectationResolverService, params Argument[] arguments)
        {
            if (originStep == null)
            {
                throw new ArgumentNullException(nameof(originStep));
            }

            if (targetStep == null)
            {
                throw new ArgumentNullException(nameof(targetStep));
            }

            if (!Steps.Contains(originStep))
            {
                throw new StepNotInProcessException(this, originStep);
            }

            if (!Steps.Contains(targetStep))
            {
                throw new StepNotInProcessException(this, targetStep);
            }

            var stepNavigator = originStep.StepNavigators.FirstOrDefault(x => x.TargetStep.Equals(targetStep));

            if (stepNavigator == null)
            {
                throw new StepNavigatorNotFoundException(this, originStep, targetStep);
            }

            return stepNavigator.CanMove(expectationResolverService, arguments);
        }

        public void AddStep(Step step)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            
            _steps.Add(step);
        }
    }
}
