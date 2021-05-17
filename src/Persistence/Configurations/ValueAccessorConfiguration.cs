using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ValueProviderConfiguration : MemberDescriptorBaseConfiguration<ValueProvider>
    {
        public override void Configure(EntityTypeBuilder<ValueProvider> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.MethodArguments);

            builder.Property(x => x.OwningType)
                .HasColumnName(nameof(ValueProvider.OwningType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }
}