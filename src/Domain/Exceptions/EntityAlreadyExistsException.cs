using System;

namespace Domain.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(object id) : base($"Entity with id: {id} - already exists.")
        {
            
        }
    }
}