using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ValueAccessorConfiguration : MemberDescriptorConfiguration<ValueAccessor>
    {
        public override void Configure(EntityTypeBuilder<ValueAccessor> builder)
        {
            builder.HasMany(x => x.MethodArguments);

            builder.Property(x => x.OwningType)
                .HasColumnName(nameof(ValueAccessor.OwningType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }
}