using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddBiggerThanExpectation
{
    public class AddBiggerThanExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddBiggerThanExpectationCommand>
    {
        public AddBiggerThanExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueAccessorRepository valueAccessorRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueAccessorRepository, expectationRepository)
        {
        }

        protected override BiggerThanExpectation CreateExpectation(AddBiggerThanExpectationCommand request, ValueAccessor valueAccessor)
        {
            return new(valueAccessor, request.Value);
        }
    }
}