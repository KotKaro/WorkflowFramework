using System;
using Domain.Common;

namespace Domain.ProcessAggregate
{
    public class MemberDescriptor : Entity
    {
        public string Name { get; private set; }
        
        protected MemberDescriptor() { }
        
        public MemberDescriptor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }
            
            Name = name;
        }
    }
}