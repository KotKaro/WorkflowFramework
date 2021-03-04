using System.Collections.Generic;

namespace WorkflowFramework.Domain.ProcessAggregate.Specification
{
    public interface ISpecification
    {
        bool Apply(object obj, IEnumerable<Argument> arguments);
    }
}