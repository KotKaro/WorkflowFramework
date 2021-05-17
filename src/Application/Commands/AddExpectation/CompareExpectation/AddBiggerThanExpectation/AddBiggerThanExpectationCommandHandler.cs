using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddBiggerThanExpectation
{
    public class AddBiggerThanExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddBiggerThanExpectationCommand>
    {
        public AddBiggerThanExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueProviderRepository valueProviderRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueProviderRepository, expectationRepository)
        {
        }

        protected override BiggerThanExpectation CreateExpectation(AddBiggerThanExpectationCommand request, ValueProvider valueProvider)
        {
            return new(valueProvider, request.Value);
        }
    }
}