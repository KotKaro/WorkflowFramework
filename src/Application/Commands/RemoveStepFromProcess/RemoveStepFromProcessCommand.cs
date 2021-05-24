using System;
using MediatR;

namespace Application.Commands.RemoveStepFromProcess
{
    public class RemoveStepFromProcessCommand : IRequest
    {
        public string ProcessName { get; set; }
        public Guid StepId { get; set; }
    }
}