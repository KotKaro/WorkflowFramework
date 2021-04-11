using Domain.ProcessAggregate.Expectations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Expectations
{
    public class ExpectationBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Expectation
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<string>("DescribedTypeFullName")
                .HasColumnName("DescribedTypeFullName");
        }
    }
}