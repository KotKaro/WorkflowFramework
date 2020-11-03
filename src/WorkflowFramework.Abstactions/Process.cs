using System;

namespace WorkflowFramework.Abstractions
{
    public class Process
    {
        private readonly string _name;

        public Process(string name) {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            _name = name;
        }
        
        
    }
}
