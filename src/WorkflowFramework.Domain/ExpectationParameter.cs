using System;
using WorkflowFramework.Abstractions;
using WorkflowFramework.Abstractions.ValueObjects;

namespace WorkflowFramework.Domain
{
    public abstract class ExpectationParameter : IExpectationParameter
    {
        public Name Name { get; }
        public Type Type { get; }
    }
}