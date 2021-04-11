using System;
using Domain.ProcessAggregate;

namespace Domain.Exceptions
{
    public class StepNotInProcessException : Exception
    {
        public StepNotInProcessException(Process process, Step step)
        : base($"Process:{ process.Name } - does not contans step: {step.Name.Value}")
        {
        }
    }
}