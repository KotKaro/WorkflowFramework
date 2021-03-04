using System.Collections.Generic;
using WorkflowFramework.Abstractions;

namespace WorkflowFramework.Domain
{
    public class ExpectationParameterValue : ValueObject
    {
        private readonly object _value;
        private readonly ExpectationParameter _parameter;

        public ExpectationParameterValue(object value, ExpectationParameter parameter)
        {
            _value = value;
            _parameter = parameter;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
            yield return _parameter;
        }
    }
}