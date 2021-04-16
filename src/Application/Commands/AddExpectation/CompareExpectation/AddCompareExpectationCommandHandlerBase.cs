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
        private readonly IValueAccessorRepository _valueAccessorRepository;
        private readonly IExpectationRepository _expectationRepository;

        protected AddCompareExpectationCommandHandlerBase(
            IStepNavigatorRepository stepNavigatorRepository,
            IValueAccessorRepository valueAccessorRepository,
            IExpectationRepository expectationRepository
        )
        {
            _stepNavigatorRepository = stepNavigatorRepository;
            _valueAccessorRepository = valueAccessorRepository;
            _expectationRepository = expectationRepository;
        }

        public virtual async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var stepNavigator = await _stepNavigatorRepository.GetByIdAsync(request.StepNavigatorId);

            if (stepNavigator is null)
            {
                throw new ObjectNotFoundException(request.StepNavigatorId, typeof(StepNavigator));
            }

            var valueAccessor = await _valueAccessorRepository.GetByIdAsync(request.ValueAccessorId);

            if (valueAccessor is null)
            {
                throw new ObjectNotFoundException(request.ValueAccessorId, typeof(ValueAccessor));
            }

            var expectation = CreateExpectation(request, valueAccessor);

            await _expectationRepository.CreateAsync(expectation);
            stepNavigator.AddExpectations(expectation);

            return Unit.Value;
        }

        protected abstract Expectation CreateExpectation(
            TCommand request,
            ValueAccessor valueAccessor
        );
    }
}