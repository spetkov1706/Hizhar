using Hizhar.Data.Models;
using Hizhar.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class TrailPointConfiguration : IEntityTypeConfiguration<TrailPoint>
    {
        public void Configure(EntityTypeBuilder<TrailPoint> builder)
        {
            builder
                .HasOne(p => p.Trail)
                .WithMany(t => t.Points)
                .HasForeignKey(p => p.TrailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedData.GetTrailPoints());
        }
    }
}
