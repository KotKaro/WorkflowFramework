using System;
using WorkflowFramework.Abstractions.ValueObjects;

namespace WorkflowFramework.Abstractions
{
    public interface IExpectationParameter
    {
        Name Name { get; }
        Type Type { get; }
    }
}