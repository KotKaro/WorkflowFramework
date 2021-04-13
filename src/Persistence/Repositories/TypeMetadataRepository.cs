using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal class TypeMetadataRepository : RepositoryBase, ITypeMetadataRepository
    {
        public TypeMetadataRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }
        
        public async Task CreateAsync(TypeMetadata typeMetadata)
        {
            await Context.AddAsync(typeMetadata);
        }

        public void Remove(TypeMetadata typeMetadata)
        {
            Context.RemoveRange(typeMetadata);
        }

        public TypeMetadata GetByType(Type type)
        {
            return Context
                .Set<TypeMetadata>()
                .FirstOrDefault(x => x.Type == type);
        }
    }
}