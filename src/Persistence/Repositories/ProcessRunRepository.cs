using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal class ProcessRunRepository : RepositoryBase, IProcessRunRepository
    {
        public ProcessRunRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(ProcessRun processRun)
        {
            await Context.AddAsync(processRun);
        }
    }
}