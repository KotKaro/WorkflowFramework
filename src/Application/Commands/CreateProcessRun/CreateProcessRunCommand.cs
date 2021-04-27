using System;
using MediatR;

namespace Application.Commands.CreateProcessRun
{
    public class CreateProcessRunCommand : IRequest
    {
        public Guid ProcessId { get; set; }

        public Guid StartStepId { get; set; }

        // ReSharper disable once InconsistentNaming
        public ArgumentDto[] ArgumentDTOs { get; set; }
    }
}