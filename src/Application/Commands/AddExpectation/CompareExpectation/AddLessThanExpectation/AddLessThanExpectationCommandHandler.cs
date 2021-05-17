using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddLessThanExpectation
{
    public class AddLessThanExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddLessThanExpectationCommand>
    {
        public AddLessThanExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueProviderRepository valueProviderRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueProviderRepository, expectationRepository)
        {
        }

        protected override LessThanExpectation CreateExpectation(AddLessThanExpectationCommand request, ValueProvider valueProvider)
        {
            return new(valueProvider, request.Value);
        }
    }
}