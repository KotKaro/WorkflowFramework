namespace Domain.ProcessAggregate.Expectations.CompareExpectations
{
    public class EqualExpectation : CompareExpectationBase
    {
        private EqualExpectation() {}
        
        public EqualExpectation(ValueAccessor valueAccessor, object value) : base(valueAccessor, value)
        {
        }

        public override bool Apply(object instance, params Argument[] arguments)
        {
            return ValueAccessor
                .GetValue(instance, arguments)
                .Equals(Value);
        }
    }
}