using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddEqualExpectation
{
    public class AddEqualExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddEqualExpectationCommand>
    {
        public AddEqualExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueAccessorRepository valueAccessorRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueAccessorRepository, expectationRepository)
        {
        }

        protected override EqualExpectation CreateExpectation(AddEqualExpectationCommand request, ValueAccessor valueAccessor)
        {
            return new(valueAccessor, request.Value);
        }
    }
}