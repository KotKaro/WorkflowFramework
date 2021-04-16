using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.AggregateExpectation.AddAndExpectation
{
    public class AddAndExpectationCommandHandler : AddAggregateExpectationCommandHandler<AddAndExpectationCommand>
    {
        public AddAndExpectationCommandHandler(
            IExpectationRepository expectationRepository,
            IStepNavigatorRepository stepNavigatorRepository
        ) : base(expectationRepository, stepNavigatorRepository)
        {
        }

        protected override AndExpectation CreateExpectation(Expectation[] expectations)
        {
            return new(expectations);
        }
    }
}