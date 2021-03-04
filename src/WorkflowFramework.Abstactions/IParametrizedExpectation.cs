using System.Collections.Generic;

namespace WorkflowFramework.Abstractions
{
    public interface IParametrizedExpectation : IExpectation
    {
        public IEnumerable<IExpectationParameter> GetParameters();
    }
}