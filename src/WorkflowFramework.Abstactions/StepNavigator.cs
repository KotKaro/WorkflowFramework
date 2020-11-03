using System;

namespace WorkflowFramework.Abstractions
{
    public class StepNavigator
    {
        private readonly Step _targetStep;

        public StepNavigator(Step targetStep)
        {
            _targetStep = targetStep ?? throw new ArgumentNullException(nameof(targetStep));
        }
    }
}
