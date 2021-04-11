using System;
using Domain.ProcessAggregate;

namespace Domain.Exceptions
{
    public class ExpectationsNotMetExcpetion : Exception
    {
        public ExpectationsNotMetExcpetion(Step originStep, Step targetStep)
        : base($"Cannot proceed navigation between steps, origin step: {originStep.Name.Value} - to step: {targetStep.Name.Value} - expectations not met")
        {
        }
    }
}
