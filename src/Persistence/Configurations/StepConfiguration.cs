using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasMany(x => x.StepNavigators)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(x => x.Value).HasColumnName(nameof(Name));
            });
        }
    }
}