using System;
using Domain.Common;

namespace Domain.ProcessAggregate.Expectations
{
    public abstract class Expectation : Entity
    {
        private string DescribedTypeFullName { get; set; } 
        
        public Type DescribedType => Type.GetType(DescribedTypeFullName);

        protected Expectation() {}
        
        protected Expectation(Type describedType) : this()
        {
            DescribedTypeFullName = $"{describedType.FullName}, {describedType.Assembly.FullName}";
        }

        public abstract bool Apply(object instance, params Argument[] arguments);
    }
}