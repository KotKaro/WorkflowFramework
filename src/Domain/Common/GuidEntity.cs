using System;

namespace Domain.Common
{
    public abstract class GuidEntity : Entity<Guid>
    {
        protected GuidEntity(Guid? id = null) : base(id ?? Guid.NewGuid())
        {
        }
    }
}