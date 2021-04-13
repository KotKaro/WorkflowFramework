using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.CreateStep
{
    public class CreateStepCommandHandler : IRequestHandler<CreateStepCommand>
    {
        private readonly IStepRepository _stepRepository;

        public CreateStepCommandHandler(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public async Task<Unit> Handle(CreateStepCommand request, CancellationToken cancellationToken)
        {
            if (await _stepRepository.GetByNameAsync(request.StepName) != null)
            {
                throw new StepWithNameExistsException(request.StepName);
            }
            
            var step = new Step(request.StepName);
            await _stepRepository.CreateAsync(step);

            return Unit.Value;
        }
    }
}