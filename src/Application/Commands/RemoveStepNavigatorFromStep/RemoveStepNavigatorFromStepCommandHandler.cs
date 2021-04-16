using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveStepNavigatorFromStep
{
    public class RemoveStepNavigatorFromStepCommandHandler : IRequestHandler<RemoveStepNavigatorFromStepCommand>
    {
        private readonly IStepRepository _stepRepository;
        private readonly IStepNavigatorRepository _stepNavigatorRepository;

        public RemoveStepNavigatorFromStepCommandHandler(IStepRepository stepRepository, IStepNavigatorRepository stepNavigatorRepository)
        {
            _stepRepository = stepRepository;
            _stepNavigatorRepository = stepNavigatorRepository;
        }

        public async Task<Unit> Handle(RemoveStepNavigatorFromStepCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepository.GetByIdAsync(request.StepId);

            if (step is null)
            {
                throw new ObjectNotFoundException(request.StepId, typeof(Step));
            }
            
            if (!step.GotStepNavigatorWithTargetStepId(request.TargetStepId))
            {
                return Unit.Value;
            }

            var stepNavigator = step.StepNavigators
                .First(x => x.TargetStep.Id == request.TargetStepId);
            
            _stepNavigatorRepository.Remove(stepNavigator);
            step.RemoveStepNavigator(stepNavigator);
            
            return Unit.Value;
        }
    }
}