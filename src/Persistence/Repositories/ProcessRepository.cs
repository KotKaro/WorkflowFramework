using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ProcessRepository : RepositoryBase, IProcessRepository
    {
        public ProcessRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }
        
        public async Task CreateAsync(Process process)
        {
            await Context.Set<Process>().AddAsync(process);
        }

        public Task<Process> GetByIdAsync(Guid id)
        {
            return Context.Set<Process>()
                .Include(x => x.Steps)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Process> GetByName(string name)
        {
            return Context.Set<Process>()
                .Include(x => x.Steps)
                .FirstOrDefaultAsync(x => x.Name.Value == name);
        }

        public IEnumerable<Process> GetAll()
        {
            return Context.Set<Process>();
        }
    }
}