using System;
using System.Text.Json;

namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public abstract class CompareExpectationBase : Expectation
    {
        public string ValueString { get; private set; }
        public Type ValueType { get; private set; }
        public ValueAccessor ValueAccessor { get; private set; }
        
        public object Value => JsonSerializer.Deserialize(ValueString, ValueType); 

        protected CompareExpectationBase() {}
        
        protected CompareExpectationBase(ValueAccessor valueAccessor, object value) : base(valueAccessor.OwningType)
        {
            ValueAccessor = valueAccessor;
            ValueString = JsonSerializer.Serialize(value);
            ValueType = value.GetType();
        }
    }
}