using Domain.Common.ValueObjects;
using Domain.Exceptions;
using Microsoft.CSharp.RuntimeBinder;

namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public class LessThanExpectation : CompareExpectationBase
    {
        private LessThanExpectation() {}
        
        public LessThanExpectation(ValueProvider valueProvider, object value) : base(valueProvider, new JsonValue(value))
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            var dynamicValue = (dynamic) Value.GetOriginalValue();
            var accessorValue = (dynamic) ValueProvider
                .GetValue(instance, arguments);

            try
            {
                return dynamicValue < accessorValue;
            }
            catch (RuntimeBinderException runtimeBinderException)
            {
                throw new CannotCompareException(dynamicValue, accessorValue, runtimeBinderException.Message);
            }
        }
    }
}