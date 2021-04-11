using Domain.Exceptions;
using Microsoft.CSharp.RuntimeBinder;

namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public class BiggerThanExpectation : CompareExpectationBase
    {
        private BiggerThanExpectation() { }
        
        public BiggerThanExpectation(ValueAccessor valueAccessor, object value) : base(valueAccessor, value)
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            var dynamicValue = (dynamic) Value;
            var accessorValue = (dynamic) ValueAccessor
                .GetValue(instance, arguments);

            try
            {
                return dynamicValue > accessorValue;
            }
            catch (RuntimeBinderException runtimeBinderException)
            {
                throw new CannotCompareException(dynamicValue, accessorValue, runtimeBinderException.Message);
            }
        }
    }
}