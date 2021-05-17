using System;
using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.ProcessAggregate
{
    public class Argument : Entity
    {
        public JsonValue Value { get; private set; }
        public MemberDescriptor MemberDescriptor { get; }

        private Argument() {}
        
        public Argument(MemberDescriptor memberDescriptor, object value)
        {
            MemberDescriptor = memberDescriptor;
            Value = new JsonValue(value);

            if (memberDescriptor.Type != Value.ValueType)
            {
                throw new ArgumentException("Member descriptor type is not the same as provided value type!");
            }
        }
    }
}