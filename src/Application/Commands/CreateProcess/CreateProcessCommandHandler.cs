using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using MediatR;
using Process = Domain.ProcessAggregate.Process;

namespace Application.Commands.CreateProcess
{
    public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand>
    {
        private readonly IProcessRepository _processRepository;

        public CreateProcessCommandHandler(IProcessRepository processRepository)
        {
            _processRepository = processRepository;
        }
        
        public async Task<Unit> Handle(CreateProcessCommand request, CancellationToken cancellationToken)
        {
            await _processRepository.CreateAsync(Process.Create(request.ProcessName, _processRepository));

            return Unit.Value;
        }
    }
}