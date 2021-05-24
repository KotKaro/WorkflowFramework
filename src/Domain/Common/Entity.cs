using System;

namespace Domain.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; private set; }
        
        protected Entity(TId id)
        {
            Id = id ?? default;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Id.Equals(((Entity<TId>)obj).Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
