using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class HutKeeperRequestConfiguration : IEntityTypeConfiguration<HutKeeperRequest>
    {
        public void Configure(EntityTypeBuilder<HutKeeperRequest> builder)
        {
            builder
                .HasOne(r => r.Hut)
                .WithMany(h => h.KeeperRequests)
                .HasForeignKey(r => r.HutId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.HutKeeperRequests)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
