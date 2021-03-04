namespace WorkflowFramework.Domain.ProcessAggregate
{
    public class Argument
    {
        public MemberDescriptor MemberDescriptor { get; }
        public object Value { get; }

        public Argument(MemberDescriptor memberDescriptor, object value)
        {
            MemberDescriptor = memberDescriptor;
            Value = value;
        }
    }
}