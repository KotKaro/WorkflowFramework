using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.ProcessAggregate.Expectations;
using Domain.Services;

namespace Domain.ProcessAggregate
{
    public class StepNavigator : GuidEntity
    {
        private ICollection<Expectation> _expectations;
        public Step TargetStep { get; }

        public IEnumerable<Expectation> Expectations => _expectations.ToArray();

        private StepNavigator()
        {
            _expectations ??= new List<Expectation>();
        }
        
        public StepNavigator(Step targetStep) : this()
        {
            TargetStep = targetStep ?? throw new ArgumentNullException(nameof(targetStep));
        }

        public bool CanMove(ExpectationResolverService expectationResolverService, params Argument[] arguments)
        {
            if (!_expectations?.Any() ?? true)
            {
                return true;
            }

            return expectationResolverService.Resolve(_expectations.ToArray(), arguments);
        }
        
        public void AddExpectations(params Expectation[] expectations)
        {
            _expectations ??= new List<Expectation>();
            foreach (var expectation in expectations)
            {
                _expectations.Add(expectation);
            }
        }
    }
}
