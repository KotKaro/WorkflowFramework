using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Domain.Common.ValueObjects
{
    public class JsonValue : ValueObject
    {
        public string ValueJson { get; private set; }
        public Type ValueType { get; private set; }

        private JsonValue()
        {
        }
        
        public JsonValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            ValueJson = JsonSerializer.Serialize(value);
            ValueType = value.GetType();

            AssertValueCanBeDeserialized();
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ValueJson;
            yield return ValueType;
        }
        
        public object GetOriginalValue()
        {
            return JsonSerializer.Deserialize(ValueJson, ValueType);
        }
        
        private void AssertValueCanBeDeserialized()
        {
            JsonSerializer.Deserialize(ValueJson, ValueType);
        }
    }
}