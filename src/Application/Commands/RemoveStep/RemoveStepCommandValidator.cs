using FluentValidation;

namespace Application.Commands.RemoveStep
{
    public class RemoveStepCommandValidator : AbstractValidator<RemoveStepCommand>
    {
        public RemoveStepCommandValidator()
        {
            RuleFor(x => x.StepId)
                .NotEmpty();
        }
    }
}