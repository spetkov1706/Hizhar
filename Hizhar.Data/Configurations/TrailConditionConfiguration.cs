using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class TrailConditionConfiguration : IEntityTypeConfiguration<TrailCondition>
    {
        public void Configure(EntityTypeBuilder<TrailCondition> builder)
        {
            builder
                .HasOne(c => c.Trail)
                .WithMany(t => t.Conditions)
                .HasForeignKey(c => c.TrailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.User)
                .WithMany(u => u.TrailConditions)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
