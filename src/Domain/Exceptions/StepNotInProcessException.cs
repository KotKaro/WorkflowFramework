using System;
using Domain.ProcessAggregate;

namespace Domain.Exceptions
{
    public class StepNotInProcessException : Exception
    {
        public StepNotInProcessException(Process process, Step step)
        : base($"Process:{ process.Name } - does not contains step: {step.Name.Value}")
        {
        }
        
        public StepNotInProcessException(Process process, Guid stepId)
            : base($"Process:{ process.Name } - does not contains step with ID: {stepId}")
        {
        }
    }
}