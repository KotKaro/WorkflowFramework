using System.Collections.Generic;
using System.Linq;

namespace Domain.ProcessAggregate.Expectations.AggregateExpectations
{
    public class AndExpectation : AggregateExpectationBase
    {
        protected AndExpectation() { }
        
        public AndExpectation(IReadOnlyCollection<Expectation> expectations) : base(expectations)
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            return Expectations.Aggregate(true, (current, expectation) => current && expectation.Apply(instance, arguments));
        }
    }
}