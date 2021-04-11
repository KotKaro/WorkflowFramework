using System.Threading;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.AddTypeMetadataCommand
{
    public class AddTypeMetadataCommandHandler : IRequestHandler<AddTypeMetadataCommand>
    {
        private readonly ITypeMetadataRepository _typeMetadataRepository;

        public AddTypeMetadataCommandHandler(ITypeMetadataRepository typeMetadataRepository)
        {
            _typeMetadataRepository = typeMetadataRepository;
        }
        
        public async Task<Unit> Handle(AddTypeMetadataCommand request, CancellationToken cancellationToken)
        {
            await _typeMetadataRepository.CreateAsync(
                new TypeMetadata(request.TypeFullName, request.AssemblyFullName)
            );
            
            return Unit.Value;
        }
    }
}