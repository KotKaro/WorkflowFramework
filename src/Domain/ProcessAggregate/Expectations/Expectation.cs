using System;
using Domain.Common;

namespace Domain.ProcessAggregate.Expectations
{
    public class Expectation : GuidEntity
    {
        public Type DescribedType { get; private set; }

        protected Expectation() {}
        
        protected Expectation(Type describedType) : this()
        {
            DescribedType = describedType ?? throw new ArgumentNullException(nameof(describedType));
        }

        public virtual bool Apply(object instance, params Argument[] arguments)
        {
            return false;
        }
    }
}