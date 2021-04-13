using System;
using MediatR;

namespace Application.Commands.AddStepToProcess
{
    public class AddStepToProcessCommand : IRequest
    {
        public Guid ProcessId { get; set; }
        public Guid StepId { get; set; }
    }
}