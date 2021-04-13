using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.HasMany(x => x.Steps)
                .WithOne()
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(x => x.Value).HasColumnName(nameof(Name));
            });
        }
    }
}