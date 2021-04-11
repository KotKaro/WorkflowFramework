using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MemberDescriptorConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : MemberDescriptor
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(nameof(ValueAccessor.Name));
        }
    }
}