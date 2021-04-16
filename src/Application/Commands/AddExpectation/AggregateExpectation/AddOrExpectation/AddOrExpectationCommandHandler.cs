using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.AggregateExpectation.AddOrExpectation
{
    public class AddOrExpectationCommandHandler : AddAggregateExpectationCommandHandler<AddOrExpectationCommand>
    {
        public AddOrExpectationCommandHandler(
            IExpectationRepository expectationRepository,
            IStepNavigatorRepository stepNavigatorRepository
        ) : base(expectationRepository, stepNavigatorRepository)
        {
        }

        protected override OrExpectation CreateExpectation(Expectation[] expectations)
        {
            return new(expectations);
        }
    }
}