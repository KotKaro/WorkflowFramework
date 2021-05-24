using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class WorkflowFrameworkDbContext : DbContext
    {
        public WorkflowFrameworkDbContext(DbContextOptions<WorkflowFrameworkDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var configurationsAssembly = typeof(WorkflowFrameworkDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
        }
    }
}