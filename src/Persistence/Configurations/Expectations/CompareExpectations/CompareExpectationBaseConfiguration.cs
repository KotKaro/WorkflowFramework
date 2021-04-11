using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Expectations.CompareExpectations
{
    public class CompareExpectationBaseConfiguration<TEntity> : ExpectationBaseConfiguration<TEntity> where TEntity : CompareExpectationBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ValueString)
                .HasColumnName(nameof(CompareExpectationBase.ValueString));

            builder.Property(x => x.ValueType)
                .HasColumnName(nameof(CompareExpectationBase.ValueType))
                .HasConversion(ConverterFactory.CreateTypeToStringConverter());
        }
    }
}