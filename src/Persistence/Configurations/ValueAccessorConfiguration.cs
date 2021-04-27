using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ValueAccessorConfiguration : IEntityTypeConfiguration<ValueAccessor>
    {
        public void Configure(EntityTypeBuilder<ValueAccessor> builder)
        {
            builder.HasMany(x => x.MethodArguments);
            
            builder.Property(x => x.Name)
                .HasColumnName(nameof(ValueAccessor.Name));

            builder.Property(x => x.OwningType)
                .HasColumnName(nameof(ValueAccessor.OwningType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
            
            builder.Property(x => x.ReturnType)
                .HasColumnName(nameof(ValueAccessor.ReturnType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }
}