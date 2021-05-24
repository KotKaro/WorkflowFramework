using System;

namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        
        protected Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
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

            return Id.Equals(((Entity)obj).Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
