using FluentValidation;

namespace Application.Commands.CreateStep
{
    public class CreateStepCommandValidator : AbstractValidator<CreateStepCommand>
    {
        public CreateStepCommandValidator()
        {
            RuleFor(x => x.StepName)
                .NotEmpty();
        }
    }
}