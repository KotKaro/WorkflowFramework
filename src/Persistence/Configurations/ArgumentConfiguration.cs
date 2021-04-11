using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ArgumentConfiguration : IEntityTypeConfiguration<Argument>
    {
        public void Configure(EntityTypeBuilder<Argument> builder)
        {
            builder.Property(x => x.ValueString)
                .HasColumnName(nameof(Argument.ValueString));

            builder.Property(x => x.ValueType)
                .HasColumnName(nameof(Argument.ValueType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());

            builder.HasOne(x => x.MemberDescriptor);
        }
    }
}