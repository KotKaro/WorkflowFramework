using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.AddStepToProcess
{
    public class AddStepToProcessCommandHandler : IRequestHandler<AddStepToProcessCommand>
    {
        private readonly IStepRepository _stepRepository;
        private readonly IProcessRepository _processRepository;

        public AddStepToProcessCommandHandler(IStepRepository stepRepository, IProcessRepository processRepository)
        {
            _stepRepository = stepRepository;
            _processRepository = processRepository;
        }

        public async Task<Unit> Handle(AddStepToProcessCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepository.GetByIdAsync(request.StepId);

            if (step is null)
            {
                throw new ObjectNotFoundException(request.StepId, typeof(Step));
            }
            
            var process = await _processRepository.GetByName(request.ProcessName);
            
            if (process is null)
            {
                throw new ObjectNotFoundException(request.ProcessName, typeof(Process));
            }

            if (!process.Steps.Contains(step))
            {
                process.AddStep(step);
            }

            return Unit.Value;
        }
    }
}