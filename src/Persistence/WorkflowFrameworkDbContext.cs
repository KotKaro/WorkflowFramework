using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
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

            modelBuilder.Ignore<Expectation>();
            modelBuilder.Ignore<AggregateExpectationBase>();
            modelBuilder.Ignore<CompareExpectationBase>();
            modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
        }
    }
}