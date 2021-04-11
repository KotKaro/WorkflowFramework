using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.ProcessAggregate.Expectations;
using Domain.Services;

namespace Domain.ProcessAggregate
{
    public class StepNavigator : Entity
    {
        private ICollection<Expectation> _expectations;
        public Step TargetStep { get; }

        public IEnumerable<Expectation> Expectations => _expectations.ToArray();

        private StepNavigator() { }
        
        public StepNavigator(Step targetStep)
        {
            TargetStep = targetStep ?? throw new ArgumentNullException(nameof(targetStep));
        }

        public StepNavigator(Step targetStep, params Expectation[] expectations) : this(targetStep)
        {
            AddExpectations(expectations);
        }

        public bool CanMove(ExpectationResolverService expectationResolverService, params Argument[] arguments)
        {
            if (!_expectations?.Any() ?? true)
            {
                return true;
            }

            return expectationResolverService.Resolve(_expectations.ToArray(), arguments);
        }
        
        private void AddExpectations(params Expectation[] expectations)
        {
            if (!expectations?.Any() ?? true)
            {
                throw new ArgumentException("Intended to create StepNavigator with expectations, but no expectations provided!");
            }

            _expectations ??= new List<Expectation>();
            foreach (var expectation in expectations)
            {
                _expectations.Add(expectation);
            }
        }
    }
}
