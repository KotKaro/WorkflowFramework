using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class StepRepository : RepositoryBase, IStepRepository
    {
        public StepRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public Task<Step> GetByIdAsync(Guid id)
        {
            return Context
                .Set<Step>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Step> GetByNameAsync(string name)
        {
            return Context
                .Set<Step>()
                .FirstOrDefaultAsync(x => x.Name.Value == name);
        }

        public async Task CreateAsync(Step step)
        {
            await Context.Set<Step>().AddAsync(step);
        }

        public void Remove(Step step)
        {
            Context.Set<Step>().Remove(step);
        }
    }
}