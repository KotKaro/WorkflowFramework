using System.Collections.Generic;
using System.Linq;

namespace Domain.ProcessAggregate.Expectations.AggregateExpectations
{
    public class OrExpectation : AggregateExpectationBase
    {
        private OrExpectation() {}
        
        public OrExpectation(IReadOnlyCollection<Expectation> expectations) : base(expectations)
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            return Expectations.Aggregate(false, (current, specification) => current || specification.Apply(instance, arguments));
        }
    }
}