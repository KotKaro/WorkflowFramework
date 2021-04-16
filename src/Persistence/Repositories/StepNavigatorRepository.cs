using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class StepNavigatorRepository : RepositoryBase, IStepNavigatorRepository
    {
        public StepNavigatorRepository(WorkflowFrameworkDbContext dbContext) : base(dbContext)
        {
        }

        public void Update(StepNavigator stepNavigator)
        {
            Context.Update(stepNavigator);
        }

        public async Task CreateAsync(StepNavigator stepNavigator)
        {
            await Context.AddAsync(stepNavigator);
        }

        public void Remove(StepNavigator stepNavigator)
        {
            Context.Remove(stepNavigator);
        }

        public Task<StepNavigator> GetByIdAsync(Guid id)
        {
            return Context.Set<StepNavigator>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}