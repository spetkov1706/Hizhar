using Hizhar.Data.Models;
using Hizhar.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class MountainConfiguration : IEntityTypeConfiguration<Mountain>
    {
        public void Configure(EntityTypeBuilder<Mountain> builder)
        {
            builder.HasIndex(m => m.Name).IsUnique();

            builder.HasData(SeedData.GetMountains());
        }
    }
}
