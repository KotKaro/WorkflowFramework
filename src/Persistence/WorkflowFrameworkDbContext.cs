using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class WorkflowFrameworkDbContext : DbContext
    {
        public WorkflowFrameworkDbContext(DbContextOptions<WorkflowFrameworkDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurationsAssembly = typeof(WorkflowFrameworkDbContext).Assembly;
            
            modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
        }
    }
}