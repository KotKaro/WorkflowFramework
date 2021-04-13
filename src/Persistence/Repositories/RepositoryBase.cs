namespace Persistence.Repositories
{
    internal abstract class RepositoryBase
    {
        protected readonly WorkflowFrameworkDbContext Context;

        protected RepositoryBase(WorkflowFrameworkDbContext context)
        {
            Context = context;
        }
    }
}