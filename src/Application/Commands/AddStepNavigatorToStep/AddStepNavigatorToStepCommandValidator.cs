using FluentValidation;

namespace Application.Commands.AddStepNavigatorToStep
{
    public class AddStepNavigatorToStepCommandValidator : AbstractValidator<AddStepNavigatorToStepCommand>
    {
        public AddStepNavigatorToStepCommandValidator()
        {
            RuleFor(x => x.StepId)
                .NotEmpty();
            
            RuleFor(x => x.TargetStepId)
                .NotEmpty();
        }
    }
}