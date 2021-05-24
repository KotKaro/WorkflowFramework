using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Exceptions;
using Domain.Services;

namespace Domain.ProcessAggregate
{
    public class ProcessRun : GuidEntity
    {
        public Process Process { get; private set; }
        public Step CurrentStep { get; private set; }

        public IEnumerable<Argument> Arguments { get; private set; }

        private ProcessRun() { }
        
        public ProcessRun(Process process, Step startStep, params Argument[] arguments)
        {
            Process = process ?? throw new ArgumentNullException(nameof(process));
            CurrentStep = startStep ?? throw new ArgumentNullException(nameof(startStep));

            if (!process.GotStep(CurrentStep))
            {
                throw new ArgumentException(
                    $"Process with ID: {process.Id} - does not contain step with ID: {CurrentStep.Id}")
                ;
            }
            
            Arguments = arguments;
        }

        public bool CanMove(Step targetStep, ExpectationResolverService expectationResolverService)
        {
            return Process.CanMove(CurrentStep, targetStep, expectationResolverService, Arguments.ToArray());
        }

        public void Move(Step targetStep, ExpectationResolverService expectationResolverService)
        {
            if (!CanMove(targetStep, expectationResolverService))
            {
                throw new ExpectationsNotMetException(CurrentStep, targetStep);
            }

            CurrentStep = targetStep;
        }
    }
}