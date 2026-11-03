using Hizhar.Data.Models;
using Hizhar.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class HutConfiguration : IEntityTypeConfiguration<Hut>
    {
        public void Configure(EntityTypeBuilder<Hut> builder)
        {
            builder
                .HasOne(h => h.Mountain)
                .WithMany(m => m.Huts)
                .HasForeignKey(h => h.MountainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Owner)
                .WithMany(u => u.OwnedHuts)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedData.GetHuts());
        }
    }
}
