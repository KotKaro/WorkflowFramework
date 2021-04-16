using System;
using MediatR;

namespace Application.Commands.RemoveStepNavigatorFromStep
{
    public class RemoveStepNavigatorFromStepCommand : IRequest
    {
        public Guid StepId { get; set; }
        public Guid TargetStepId { get; set; }
    }
}