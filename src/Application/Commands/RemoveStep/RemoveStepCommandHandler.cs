using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveStep
{
    public class RemoveStepCommandHandler : IRequestHandler<RemoveStepCommand>
    {
        private readonly IStepRepository _stepRepository;

        public RemoveStepCommandHandler(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public async Task<Unit> Handle(RemoveStepCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepository.GetByIdAsync(request.StepId);

            if (step != null)
            {
                _stepRepository.Remove(step);
            }
            
            return Unit.Value;
        }
    }
}