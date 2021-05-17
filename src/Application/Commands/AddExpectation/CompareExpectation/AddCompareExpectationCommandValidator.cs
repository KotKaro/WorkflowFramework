using FluentValidation;

namespace Application.Commands.AddExpectation.CompareExpectation
{
    public abstract class AddCompareExpectationCommandValidator<TCommand> 
        : AbstractValidator<TCommand>
        where TCommand : AddCompareExpectationCommand
    {
        protected AddCompareExpectationCommandValidator()
        {
            RuleFor(x => x.StepNavigatorId)
                .NotEmpty();

            RuleFor(x => x.ValueProviderId)
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotNull();
        }
    }
}