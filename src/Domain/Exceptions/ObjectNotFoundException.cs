using System;

namespace Domain.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(Guid id, Type entityType)
            : base($"Object of type: {entityType.Name} - with id: {id} - wasn't found.")
        {
        }
    }
}