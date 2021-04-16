using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;

namespace Application.Commands.AddExpectation.CompareExpectation.AddLessThanExpectation
{
    public class AddLessThanExpectationCommandHandler : AddCompareExpectationCommandHandlerBase<AddLessThanExpectationCommand>
    {
        public AddLessThanExpectationCommandHandler(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueAccessorRepository valueAccessorRepository,
            IExpectationRepository expectationRepository
        ) : base(stepNavigatorRepository, valueAccessorRepository, expectationRepository)
        {
        }

        protected override LessThanExpectation CreateExpectation(AddLessThanExpectationCommand request, ValueAccessor valueAccessor)
        {
            return new(valueAccessor, request.Value);
        }
    }
}