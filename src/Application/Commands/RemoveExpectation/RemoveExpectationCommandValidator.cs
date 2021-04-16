using FluentValidation;

namespace Application.Commands.RemoveExpectation
{
    public class RemoveExpectationCommandValidator : AbstractValidator<RemoveExpectationCommand>
    {
        public RemoveExpectationCommandValidator()
        {
            RuleFor(x => x.ExpectationId)
                .NotEmpty();
        }
    }
}