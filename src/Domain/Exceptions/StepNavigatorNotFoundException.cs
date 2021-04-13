using System;
using Domain.ProcessAggregate;

namespace Domain.Exceptions
{
    public class StepNavigatorNotFoundException : Exception
    {
        public StepNavigatorNotFoundException(Step originStep, Step targetStep)
        : base($"Cannot proceed navigation between steps, origin step: {originStep.Name.Value} - to step: {targetStep.Name.Value} - step navigator not found.")
        {
        }
    }
}
