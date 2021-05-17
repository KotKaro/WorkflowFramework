using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddEqualExpectation
{
    public class AddEqualExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddEqualExpectationCommand>
    {
        public AddEqualExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueProviderRepository valueProviderRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueProviderRepository, expectationRepository)
        {
        }

        protected override EqualExpectation CreateExpectation(AddEqualExpectationCommand request, ValueProvider valueProvider)
        {
            return new(valueProvider, request.Value);
        }
    }
}