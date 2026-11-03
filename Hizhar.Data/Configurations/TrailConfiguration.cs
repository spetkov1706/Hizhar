using Hizhar.Data.Models;
using Hizhar.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class TrailConfiguration : IEntityTypeConfiguration<Trail>
    {
        public void Configure(EntityTypeBuilder<Trail> builder)
        {
            builder
                .HasOne(t => t.Mountain)
                .WithMany(m => m.Trails)
                .HasForeignKey(t => t.MountainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedData.GetTrails());
        }
    }
}
