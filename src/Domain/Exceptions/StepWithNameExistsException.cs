using System;

namespace Domain.Exceptions
{
    public class StepWithNameExistsException : Exception
    {
        public StepWithNameExistsException(string name) : base($"Step with name: {name} - already exists.")
        {
            
        }
    }
}