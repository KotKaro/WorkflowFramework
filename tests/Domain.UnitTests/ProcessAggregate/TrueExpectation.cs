using System;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;

namespace Domain.UnitTests.ProcessAggregate
{
    public class TrueExpectation : Expectation
    {
        public int InvokeCounter { get; private set; } = 0;

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