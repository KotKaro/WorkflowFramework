using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.AddStepNavigatorToStep
{
    public class AddStepNavigatorToStepCommandHandler : IRequestHandler<AddStepNavigatorToStepCommand>
    {
        private readonly IStepRepository _stepRepository;
        private readonly IStepNavigatorRepository _stepNavigatorRepository;

        public AddStepNavigatorToStepCommandHandler(IStepRepository stepRepository, IStepNavigatorRepository stepNavigatorRepository)
        {
            _stepRepository = stepRepository;
            _stepNavigatorRepository = stepNavigatorRepository;
        }

        public async Task<Unit> Handle(AddStepNavigatorToStepCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepository.GetByIdAsync(request.StepId);

            if (step is null)
            {
                throw new ObjectNotFoundException(request.StepId, typeof(Step));
            }
            
            var targetStep = await _stepRepository.GetByIdAsync(request.TargetStepId);

            if (targetStep is null)
            {
                throw new ObjectNotFoundException(request.TargetStepId, typeof(Step));
            }

            if (step.GotStepNavigatorWithTargetStepId(targetStep.Id))
            {
                return Unit.Value;
            }
            
            var stepNavigator = new StepNavigator(targetStep);
            await _stepNavigatorRepository.CreateAsync(stepNavigator);
            
            step.AddStepNavigators(stepNavigator);
            
            return Unit.Value;
        }
    }
}