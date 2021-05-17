using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.AddExpectation.CompareExpectation
{
    public abstract class AddCompareExpectationCommandHandlerBase<TCommand>
        : IRequestHandler<TCommand>
        where TCommand : AddCompareExpectationCommand
    {
        private readonly IStepNavigatorRepository _stepNavigatorRepository;
        private readonly IValueProviderRepository _valueProviderRepository;
        private readonly IExpectationRepository _expectationRepository;

        protected AddCompareExpectationCommandHandlerBase(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueProviderRepository valueProviderRepository,
            IExpectationRepository expectationRepository
        )
        {
            _stepNavigatorRepository = stepNavigatorRepository;
            _valueProviderRepository = valueProviderRepository;
            _expectationRepository = expectationRepository;
        }

        public virtual async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var stepNavigator = await _stepNavigatorRepository.GetByIdAsync(request.StepNavigatorId);

            if (stepNavigator is null)
            {
                throw new ObjectNotFoundException(request.StepNavigatorId, typeof(StepNavigator));
            }

            var ValueProvider = await _valueProviderRepository.GetByIdAsync(request.ValueProviderId);

            if (ValueProvider is null)
            {
                throw new ObjectNotFoundException(request.ValueProviderId, typeof(ValueProvider));
            }

            var expectation = CreateExpectation(request, ValueProvider);

            await _expectationRepository.CreateAsync(expectation);
            stepNavigator.AddExpectations(expectation);

            return Unit.Value;
        }

        protected abstract Expectation CreateExpectation(
            TCommand request,
            ValueProvider valueProvider
        );
    }
}