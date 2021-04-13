using FluentValidation;

namespace Application.Commands.CreateProcess
{
    public class CreateProcessCommandValidator : AbstractValidator<CreateProcessCommand>
    {
        public CreateProcessCommandValidator()
        {
            RuleFor(x => x.ProcessName)
                .NotEmpty();
        }
    }
}