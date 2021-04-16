using System.Linq;
using FluentValidation;

namespace Application.Commands.AddExpectation.AggregateExpectation
{
    public class AddAggregateExpectationCommandValidator<TCommand>
        : AbstractValidator<TCommand>
        where TCommand : AddAggregateExpectationCommand
    {
        protected AddAggregateExpectationCommandValidator()
        {
            RuleFor(x => x.ExpectationIds)
                .NotNull()
                .NotEmpty()
                .Must(x => x?.Count() >= 2)
                .WithMessage("Cannot create and expectation with less than two expectations");

            RuleFor(x => x.StepNavigatorId)
                .NotEmpty();
        }
    }
}