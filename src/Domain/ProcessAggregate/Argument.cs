using Domain.Common;
using System.Text.Json;

namespace Domain.ProcessAggregate
{
    public class Argument : Entity
    {
        public string ValueString { get; private set; }
        public MemberDescriptor MemberDescriptor { get; }

        public object Value => JsonSerializer.Deserialize(ValueString, MemberDescriptor.Type);

        private Argument() {}
        
        public Argument(MemberDescriptor memberDescriptor, object value)
        {
            MemberDescriptor = memberDescriptor;
            ValueString = JsonSerializer.Serialize(value);
            
            AssertValueCanBeDeserialized();
        }

        private void AssertValueCanBeDeserialized()
        {
            JsonSerializer.Deserialize(ValueString, MemberDescriptor.Type);
        }
    }
}