using System;
using Domain.Common;
using System.Text.Json;

namespace Domain.ProcessAggregate
{
    public class Argument : Entity
    {
        public string ValueString { get; private set; }
        public Type ValueType { get; private set; }
        public MemberDescriptor MemberDescriptor { get; }

        public object Value => JsonSerializer.Deserialize(ValueString, ValueType);

        private Argument() {}
        
        public Argument(MemberDescriptor memberDescriptor, object value)
        {
            MemberDescriptor = memberDescriptor;
            ValueType = value.GetType();
            ValueString = JsonSerializer.Serialize(value);
        }
    }
}