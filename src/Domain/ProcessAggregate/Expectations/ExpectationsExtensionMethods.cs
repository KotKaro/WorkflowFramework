using System;
using System.Collections.Generic;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;

namespace Domain.ProcessAggregate.Expectations
{
    public static class ExpectationsExtensionMethods
    {
        public static Type GetExpectationDescribedType(this IEnumerable<Expectation> expectations)
        {
            foreach (var expectation in expectations)
            {
                if (expectation is AggregateExpectationBase aggregateExpectationBase)
                {
                    return GetExpectationDescribedType(aggregateExpectationBase.Expectations);
                }

                if (expectation.DescribedType != null)
                {
                    return expectation.DescribedType;
                }
            }

            throw new ArgumentException("Cannot find expectation with provided described type!");
        }
    }
}