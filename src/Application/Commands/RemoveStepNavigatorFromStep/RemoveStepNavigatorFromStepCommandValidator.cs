using FluentValidation;

namespace Application.Commands.RemoveStepNavigatorFromStep
{
    public class RemoveStepNavigatorFromStepCommandValidator : AbstractValidator<RemoveStepNavigatorFromStepCommand>
    {
        public RemoveStepNavigatorFromStepCommandValidator()
        {
            RuleFor(x => x.StepId)
                .NotEmpty();
            
            RuleFor(x => x.TargetStepId)
                .NotEmpty();
        }
    }
}