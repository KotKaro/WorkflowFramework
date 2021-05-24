using System;
using MediatR;

namespace Application.Commands.AddStepToProcess
{
    public class AddStepToProcessCommand : IRequest
    {
        public string ProcessName { get; set; }
        public Guid StepId { get; set; }
    }
}