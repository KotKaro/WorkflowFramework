using FluentValidation;

namespace Application.Commands.AddStepToProcess
{
    public class AddStepToProcessValidator : AbstractValidator<AddStepToProcessCommand>
    {
        public AddStepToProcessValidator()
        {
            RuleFor(x => x.ProcessId)
                .NotEmpty();
            
            RuleFor(x => x.StepId)
                .NotEmpty();
        }
    }
}