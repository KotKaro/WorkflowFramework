using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal class StepNavigatorRepository : RepositoryBase, IStepNavigatorRepository
    {
        public StepNavigatorRepository(WorkflowFrameworkDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task CreateAsync(StepNavigator stepNavigator)
        {
            await Context.AddAsync(stepNavigator);
        }
    }
}