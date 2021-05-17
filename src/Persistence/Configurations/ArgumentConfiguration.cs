using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ArgumentConfiguration : IEntityTypeConfiguration<Argument>
    {
        public void Configure(EntityTypeBuilder<Argument> builder)
        {
            builder.OwnsOne(x => x.Value, p =>
            {
                p.Property(x => x.ValueJson)
                    .HasColumnName(nameof(JsonValue.ValueJson));

                p.Property(x => x.ValueType)
                    .HasColumnName(nameof(JsonValue.ValueType))
                    .HasConversion(ConverterFactory.CreateTypeToStringConverter());
            });

            builder.HasOne(x => x.MemberDescriptor);
        }
    }
}