using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class ClimbedPeakConfiguration : IEntityTypeConfiguration<ClimbedPeak>
    {
        public void Configure(EntityTypeBuilder<ClimbedPeak> builder)
        {
            builder.HasKey(cp => new { cp.UserId, cp.PeakId });

            builder.Property(cp => cp.ClimbedOn).HasColumnType("date");

            builder
                .HasOne(cp => cp.User)
                .WithMany(u => u.ClimbedPeaks)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(cp => cp.Peak)
                .WithMany(p => p.ClimbedBy)
                .HasForeignKey(cp => cp.PeakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
