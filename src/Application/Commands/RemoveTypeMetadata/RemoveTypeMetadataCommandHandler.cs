using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveTypeMetadata
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
            var type = Type.GetType($"{request.TypeFullName},{request.AssemblyFullName}", false, false);

            var typeMetadata = _typeMetadataRepository.GetByType(type);
            
            _typeMetadataRepository.Remove(typeMetadata);
            
            return Task.FromResult(Unit.Value);
        }
    }
}