using Domain.Common.ValueObjects;

namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public abstract class CompareExpectationBase : Expectation
    {
        public JsonValue Value { get; private set; }
        public ValueProvider ValueProvider { get; private set; }
        
        protected CompareExpectationBase() {}
        
        protected CompareExpectationBase(ValueProvider valueProvider, JsonValue value) : base(valueProvider.OwningType)
        {
            ValueProvider = valueProvider;
            Value = value;
        }

        protected object GetOriginalValue()
        {
            return Value.GetOriginalValue();
        }
    }
}