using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class PeakConfiguration : IEntityTypeConfiguration<Peak>
    {
        public void Configure(EntityTypeBuilder<Peak> builder)
        {
            builder
                .HasOne(p => p.Mountain)
                .WithMany(m => m.Peaks)
                .HasForeignKey(p => p.MountainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
