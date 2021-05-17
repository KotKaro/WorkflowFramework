using System;

namespace Domain.ProcessAggregate.Expectations
{
    public class ExpectationNegation : Expectation
    {
        public Expectation Expectation { get; private set; }

        public ExpectationNegation(Expectation expectation)
        {
            Expectation = expectation ?? throw new ArgumentNullException(nameof(expectation));
        }
        
        public override bool Apply(object instance, params Argument[] arguments)
        {
            return !Expectation.Apply(instance, arguments);
        }
    }
}