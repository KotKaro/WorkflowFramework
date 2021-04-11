using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveTypeMetadataCommand
{
    public class RemoveTypeMetadataCommandHandler : IRequestHandler<RemoveTypeMetadataCommand>
    {
        private readonly ITypeMetadataRepository _typeMetadataRepository;

        public RemoveTypeMetadataCommandHandler(ITypeMetadataRepository typeMetadataRepository)
        {
            _typeMetadataRepository = typeMetadataRepository;
        }
        
        public Task<Unit> Handle(RemoveTypeMetadataCommand request, CancellationToken cancellationToken)
        {
            _typeMetadataRepository.Remove(
                new TypeMetadataDto
                {
                    TypeFullName = request.TypeFullName,
                    AssemblyFullName = request.AssemblyFullName
                }
            );
            
            return Task.FromResult(Unit.Value);
        }
    }
}