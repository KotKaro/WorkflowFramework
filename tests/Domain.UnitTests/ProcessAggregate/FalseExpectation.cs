using System;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;

namespace Domain.UnitTests.ProcessAggregate
{
    public class FalseExpectation : Expectation
    {
        public int InvokeCounter { get; private set; }

        public FalseExpectation(Type describedType) : base(describedType)
        {
        }
        
        public override bool Apply(object instance, params Argument[] arguments)
        {
            InvokeCounter += 1;
            return false;
        }
    }
}