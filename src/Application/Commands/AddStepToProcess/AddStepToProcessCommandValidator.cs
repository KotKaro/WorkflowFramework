using FluentValidation;

namespace Application.Commands.AddStepToProcess
{
    public class AddStepToProcessValidator : AbstractValidator<AddStepToProcessCommand>
    {
        public AddStepToProcessValidator()
        {
            RuleFor(x => x.ProcessName)
                .NotEmpty();
            
            RuleFor(x => x.StepId)
                .NotEmpty();
        }
    }
}