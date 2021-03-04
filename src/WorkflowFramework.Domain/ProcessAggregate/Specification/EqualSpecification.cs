using System.Collections.Generic;
using System.Linq;

namespace WorkflowFramework.Domain.ProcessAggregate.Specification
{
    public class EqualSpecification : ISpecification
    {
        private readonly ValueAccessor _valueAccessor;
        private readonly object _value;

        public EqualSpecification(ValueAccessor valueAccessor, object value)
        {
            _valueAccessor = valueAccessor;
            _value = value;
        }

        public bool Apply(object obj, IEnumerable<Argument> arguments)
        {
            return _valueAccessor.GetValue(obj, arguments.ToArray()).Equals(_value);
        }
    }
}