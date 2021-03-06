using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Domain.ProcessAggregate
{
    public class Process : Entity
    {
        private readonly ICollection<Step> _steps;

        public Name Name { get; }
        public IEnumerable<Step> Steps => _steps.ToArray();

        public static Process Create(Name name, IProcessRepository processRepository)
        {
            var process = processRepository.GetByName(name.Value).Result;

            if (process != null)
            {
                throw new EntityAlreadyExistsException(name.Value);
            }

            return new Process(name);
        }
        
        public static Process Create(Name name, ICollection<Step> steps, IProcessRepository processRepository)
        {
            var process = processRepository.GetByName(name.Value).Result;

            if (process != null)
            {
                throw new EntityAlreadyExistsException(name.Value);
            }

            return new Process(name, steps);
        }
        
        private Process() : base()
        {
            _steps ??= new List<Step>();
        }
        
        private Process(Name name) : this()
        {
            Name = name ?? throw new ArgumentException("Name not provided!");
        }
        
        private Process(Name name, ICollection<Step> steps) : this(name)
        {
            if (!steps?.Any() ?? true)
            {
                throw new ArgumentException("Process steps not provided!", nameof(steps));
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));

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
                throw new StepNavigatorNotFoundException(originStep, targetStep);
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

        public void RemoveStep(Step step)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }

            if (!_steps.Contains(step))
            {
                return;
            }
            
            var stepToRemove = _steps.First(x => x.Equals(step));
            _steps.Remove(stepToRemove);
        }

        public bool GotStep(Step step)
        {
            return _steps.Contains(step);
        }

        public Step GetStep(Guid stepId)
        {
            return Steps.FirstOrDefault(x => x.Id == stepId) 
                   ?? throw new StepNotInProcessException(this, stepId);
        }

        public bool GotStep(Guid stepId)
        {
            return _steps.Any(x => x.Id == stepId);
        }
    }
}
