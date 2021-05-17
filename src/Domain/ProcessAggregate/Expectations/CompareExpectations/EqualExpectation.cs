using Domain.Common.ValueObjects;

namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public class EqualExpectation : CompareExpectationBase
    {
        private EqualExpectation() {}
        
        public EqualExpectation(ValueProvider valueProvider, object value) : base(valueProvider, new JsonValue(value))
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            return ValueProvider
                .GetValue(instance, arguments)
                .Equals(GetOriginalValue());
        }
    }
}