using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TypeMetadataConfiguration : IEntityTypeConfiguration<TypeMetadata>
    {
        public void Configure(EntityTypeBuilder<TypeMetadata> builder)
        {
            builder.Property(x => x.Type)
                .HasColumnName(nameof(TypeMetadata.Type))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());

            builder.HasKey(x => x.Type);

            builder.HasMany(x => x.ValueProviders)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}