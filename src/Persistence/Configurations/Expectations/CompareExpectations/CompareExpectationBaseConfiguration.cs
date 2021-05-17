using Domain.Common.ValueObjects;
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

            builder.OwnsOne(x => x.Value, p =>
            {
                p.Property(x => x.ValueJson)
                    .HasColumnName(nameof(JsonValue.ValueJson));

                p.Property(x => x.ValueType)
                    .HasConversion(ConverterFactory.CreateTypeToStringConverter())
                    .HasColumnName(nameof(JsonValue.ValueType));
            });
        }
    }
}