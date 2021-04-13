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

        public void Remove(TypeMetadata typeMetadata)
        {
            _dbContext.RemoveRange(typeMetadata);
        }

        public TypeMetadata GetByType(Type type)
        {
            return _dbContext
                .Set<TypeMetadata>()
                .FirstOrDefault(x => x.Type == type);
        }
    }
}