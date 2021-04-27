using System;
using System.Linq;
using FluentValidation;

namespace Application.Commands.CreateProcessRun
{
    public class CreateProcessRunCommandValidator : AbstractValidator<CreateProcessRunCommand>
    {
        public CreateProcessRunCommandValidator()
        {
            RuleFor(x => x.ProcessId)
                .NotEmpty();
            
            RuleFor(x => x.StartStepId)
                .NotEmpty();

            RuleFor(x => x.ArgumentDTOs)
                .Must(x => x?.All(argumentDto => argumentDto.MemberDescriptorId != Guid.Empty) ?? true);
        }
    }
}