using System;
using MediatR;

namespace Application.Commands.RemoveStep
{
    public class RemoveStepCommand : IRequest
    {
        public Guid StepId { get; set; }
    }
}