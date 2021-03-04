using System;
using System.Collections.Generic;

namespace WorkflowFramework.Domain.Exceptions
{
    public class ValueNotReachableException : Exception
    {
        public ValueNotReachableException(string propertyName, object instance)
            : base($"Cannot access property: {propertyName} - in instance of type: {instance.GetType().FullName}")
        {
            
        }
        
        public ValueNotReachableException(string methodName, IEnumerable<string> argumentNames , object instance)
            : base($"Cannot access method: {methodName} - in instance of type: {instance.GetType().FullName} - with arguments: {string.Join(',', argumentNames)}")
        {
            
        }
    }
}