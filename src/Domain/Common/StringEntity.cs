using System;

namespace Domain.Common
{
    public abstract class StringEntity : Entity<string>
    {
        protected StringEntity(string id) : base(id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("ID cannot be null or whitespace!");
            }
        }
    }
}