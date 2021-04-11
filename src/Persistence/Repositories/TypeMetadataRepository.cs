using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class TypeMetadataRepository : ITypeMetadataRepository
    {
        private readonly WorkflowFrameworkDbContext _dbContext;

        public TypeMetadataRepository(WorkflowFrameworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task CreateAsync(TypeMetadata typeMetadata)
        {
            await _dbContext.AddAsync(typeMetadata);
        }

        public void Remove(TypeMetadataDto typeMetadataDto)
        {
            var typeMetadataToRemove = _dbContext.Set<TypeMetadata>()
                .Where(x => x.Type ==
                            Type.GetType($"{typeMetadataDto.TypeFullName},{typeMetadataDto.AssemblyFullName}"));
            
            _dbContext.RemoveRange(typeMetadataToRemove);
        }
    }
}