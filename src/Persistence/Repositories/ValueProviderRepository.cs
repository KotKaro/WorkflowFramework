using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ValueProviderRepository : RepositoryBase, IValueProviderRepository
    {
        public ValueProviderRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public Task<ValueProvider> GetByIdAsync(Guid id)
        {
            return Context.Set<ValueProvider>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}