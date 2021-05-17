using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MemberDescriptorBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : MemberDescriptor
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(nameof(MemberDescriptor.Name));

            builder.Property(x => x.Type)
                .HasColumnName(nameof(MemberDescriptor.Type))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }

    public class MemberDescriptorConfiguration : MemberDescriptorBaseConfiguration<MemberDescriptor>
    {
        public override void Configure(EntityTypeBuilder<MemberDescriptor> builder)
        {
            base.Configure(builder);
            
            builder.HasKey(x => x.Id);
        }
    }
}