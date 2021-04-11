using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StepNavigatorConfiguration : IEntityTypeConfiguration<StepNavigator>
    {
        public void Configure(EntityTypeBuilder<StepNavigator> builder)
        {
            builder.HasOne(x => x.TargetStep);
            
            builder.HasMany(x => x.Expectations)
                .WithOne()
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}