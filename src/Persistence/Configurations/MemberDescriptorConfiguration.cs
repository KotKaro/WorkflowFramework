using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MemberDescriptorConfiguration : IEntityTypeConfiguration<MemberDescriptor>
    {
        public virtual void Configure(EntityTypeBuilder<MemberDescriptor> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(nameof(MemberDescriptor.Name));

            builder.Property(x => x.Type)
                .HasColumnName(nameof(MemberDescriptor.Type))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }
}