namespace Domain.Common
{
    public class NullValue
    {
        public static NullValue Create()
        {
            return new();
        }
        
        private NullValue() {}
    }
}