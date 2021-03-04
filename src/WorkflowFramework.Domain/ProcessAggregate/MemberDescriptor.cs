using System;

namespace WorkflowFramework.Domain.ProcessAggregate
{
    public class MemberDescriptor
    {
        public string Name { get; }
        
        public Type OwningType { get; }
        
        public MemberDescriptor(string name, Type owningType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }
            
            Name = name;
            OwningType = owningType ?? throw new ArgumentNullException(nameof(owningType));
        }
    }
}