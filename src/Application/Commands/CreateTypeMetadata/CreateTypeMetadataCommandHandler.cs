using System.Threading;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.CreateTypeMetadata
{
    public class CreateTypeMetadataCommandHandler : IRequestHandler<CreateTypeMetadataCommand>
    {
        private readonly ITypeMetadataRepository _typeMetadataRepository;

        public CreateTypeMetadataCommandHandler(ITypeMetadataRepository typeMetadataRepository)
        {
            _typeMetadataRepository = typeMetadataRepository;
        }
        
        public async Task<Unit> Handle(CreateTypeMetadataCommand request, CancellationToken cancellationToken)
        {
            await _typeMetadataRepository.CreateAsync(
                new TypeMetadata(request.TypeFullName, request.AssemblyFullName)
            );
            
            return Unit.Value;
        }
    }
}