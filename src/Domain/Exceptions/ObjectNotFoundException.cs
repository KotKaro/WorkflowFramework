using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(Guid id, Type entityType)
            : base($"Object of type: {entityType.Name} - with id: {id} - wasn't found.")
        {
        }
        
        public ObjectNotFoundException(string id, Type entityType)
            : base($"Object of type: {entityType.Name} - with id: {id} - wasn't found.")
        {
        }
        
        public ObjectNotFoundException(IEnumerable<Guid> ids, Type entityType)
            : base($"Objects of type: {entityType.Name} - with ids: {string.Join(',', ids.Select(x => x.ToString()))} - wasn't found.")
        {
        }
    }
}