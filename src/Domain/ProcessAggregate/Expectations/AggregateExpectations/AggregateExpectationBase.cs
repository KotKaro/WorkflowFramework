using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain.ProcessAggregate.Expectations.AggregateExpectations
{
    public abstract class AggregateExpectationBase : Expectation
    {
        public  IReadOnlyCollection<Expectation> Expectations { get; private set; }
        
        protected AggregateExpectationBase() {}
        
        protected AggregateExpectationBase(IReadOnlyCollection<Expectation> expectations) : base(expectations.GetExpectationDescribedType())
        {
            if (!expectations.Any())
            {
                throw new InvalidOperationException("Cannot apply aggregate specification for less than two specifications.");
            }

            if (expectations.Select(x => x.DescribedType).Distinct().Count() > 1)
            {
                throw new AmbiguousSpecificationsTypesException();
            }
            
            Expectations = expectations;
        }
    }
}