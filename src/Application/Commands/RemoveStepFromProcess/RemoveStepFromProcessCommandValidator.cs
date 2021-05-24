using Application.Commands.AddStepToProcess;
using FluentValidation;

namespace Application.Commands.RemoveStepFromProcess
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