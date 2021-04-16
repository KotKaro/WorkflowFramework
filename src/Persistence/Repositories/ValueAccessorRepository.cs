using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ValueAccessorRepository : RepositoryBase, IValueAccessorRepository
    {
        public ValueAccessorRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public Task<ValueAccessor> GetByIdAsync(Guid id)
        {
            return Context.Set<ValueAccessor>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}