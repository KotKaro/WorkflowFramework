using System;

namespace Domain.Exceptions
{
    public class CannotCompareException : Exception
    {
        public CannotCompareException(object val1, object val2, string originalMessage = null)
            : base(
                @$"Cannot compare values: {val1} - {val2} 
                    {(val1 != null && val2 != null ? $" - of types: {val1?.GetType()} - {val2.GetType()}" : string.Empty)}
                    {(!string.IsNullOrWhiteSpace(originalMessage) ? $"- Original message: {originalMessage}" : string.Empty)}"
            )
        {
        }
    }
}