using System;
using Domain.ProcessAggregate;

namespace Domain.Exceptions
{
    public class ExpectationsNotMetException : Exception
    {
        public ExpectationsNotMetException(Step originStep, Step targetStep)
        : base($"Cannot proceed navigation between steps, origin step: {originStep.Name.Value} - to step: {targetStep.Name.Value} - expectations not met")
        {
        }
    }
}
