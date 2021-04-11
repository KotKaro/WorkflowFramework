using Domain.ProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProcessRunConfiguration : IEntityTypeConfiguration<ProcessRun>
    {
        public void Configure(EntityTypeBuilder<ProcessRun> builder)
        {
            builder.HasOne(x => x.Process)
                .WithMany();

            builder.HasOne(x => x.CurrentStep);

            builder.HasMany(x => x.Arguments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}