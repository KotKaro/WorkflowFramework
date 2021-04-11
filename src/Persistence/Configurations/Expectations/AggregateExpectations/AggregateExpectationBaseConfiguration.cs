using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Expectations.AggregateExpectations
{
    public class AggregateExpectationBaseConfiguration<TEntity> : ExpectationBaseConfiguration<TEntity> where TEntity : AggregateExpectationBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            
            builder.HasMany(x => x.Expectations);
        }
    }
}