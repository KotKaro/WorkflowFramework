using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveStepFromProcess
{
    public class RemoveStepFromProcessCommandHandler : IRequestHandler<RemoveStepFromProcessCommand>
    {
        private readonly IStepRepository _stepRepository;
        private readonly IProcessRepository _processRepository;

        public RemoveStepFromProcessCommandHandler(IStepRepository stepRepository, IProcessRepository processRepository)
        {
            _stepRepository = stepRepository;
            _processRepository = processRepository;
        }

        public async Task<Unit> Handle(RemoveStepFromProcessCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepository.GetByIdAsync(request.StepId);

            if (step is null)
            {
                throw new ObjectNotFoundException(request.StepId, typeof(Step));
            }
            
            var process = await _processRepository.GetByIdAsync(request.ProcessId);
            
            if (process is null)
            {
                throw new ObjectNotFoundException(request.ProcessId, typeof(Process));
            }

            if (process.Steps.Contains(step))
            {
                process.RemoveStep(step);
            }

            return Unit.Value;
        }
    }
}