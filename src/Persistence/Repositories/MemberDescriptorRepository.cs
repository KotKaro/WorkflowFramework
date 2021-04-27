using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class MemberDescriptorRepository : RepositoryBase, IMemberDescriptorRepository
    {
        public MemberDescriptorRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public Task<MemberDescriptor> GetByIdAsync(Guid id)
        {
            return Context.Set<MemberDescriptor>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}