using System;
using Domain.ProcessAggregate;

namespace Domain.UnitTests.ProcessAggregate
{
    public class TrueExpectation : Domain.ProcessAggregate.Expectations.Expectation
    {
        public int InvokeCounter { get; private set; }

        public TrueExpectation(Type describedType) : base(describedType)
        {
        }
        
        public override bool Apply(object instance, params Argument[] arguments)
        {
            InvokeCounter += 1;
            return true;
        }
    }
}