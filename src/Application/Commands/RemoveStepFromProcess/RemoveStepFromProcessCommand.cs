using System;
using MediatR;

namespace Application.Commands.RemoveStepFromProcess
{
    public class RemoveStepFromProcessCommand : IRequest
    {
        public Guid ProcessId { get; set; }
        public Guid StepId { get; set; }
    }
}