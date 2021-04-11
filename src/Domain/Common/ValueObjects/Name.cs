using System;
using System.Collections.Generic;

namespace Domain.Common.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace", nameof(value));
            }

            Value = value;
        }

        public static implicit operator Name(string name) => new(name);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
