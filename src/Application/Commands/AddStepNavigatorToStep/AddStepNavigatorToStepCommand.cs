using System;
using MediatR;

namespace Application.Commands.AddStepNavigatorToStep
{
    public class AddStepNavigatorToStepCommand : IRequest
    {
        public Guid StepId { get; set; }
        public Guid TargetStepId { get; set; }
    }
}