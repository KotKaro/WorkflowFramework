using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.AddExpectation.AggregateExpectation
{
    public abstract class AddAggregateExpectationCommandHandler<TCommand>
        : IRequestHandler<TCommand>
        where TCommand : AddAggregateExpectationCommand 
    {
        private readonly IExpectationRepository _expectationRepository;
        private readonly IStepNavigatorRepository _stepNavigatorRepository;

        protected AddAggregateExpectationCommandHandler(
            IExpectationRepository expectationRepository,
            IStepNavigatorRepository stepNavigatorRepository
        )
        {
            _expectationRepository = expectationRepository;
            _stepNavigatorRepository = stepNavigatorRepository;
        }
        
        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var stepNavigator = await _stepNavigatorRepository.GetByIdAsync(request.StepNavigatorId);

            if (stepNavigator is null)
            {
                throw new ObjectNotFoundException(request.StepNavigatorId, typeof(StepNavigator));
            }

            var expectations = _expectationRepository.GetByIds(request.ExpectationIds)
                .ToArray();

            var notPresentIds = request.ExpectationIds
                .Except(expectations.Select(x => x.Id))
                .ToArray();
            
            if (notPresentIds.Any())
            {
                throw new ObjectNotFoundException(notPresentIds, typeof(Expectation));
            }

            var expectation = CreateExpectation(expectations);
            await _expectationRepository.CreateAsync(expectation);
            
            stepNavigator.AddExpectations(expectation);
            _stepNavigatorRepository.Update(stepNavigator);
            
            return Unit.Value;
        }

        protected abstract Expectation CreateExpectation(
            Expectation[] expectations
        );
    }
}