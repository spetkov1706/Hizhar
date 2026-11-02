using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class FavoriteHutConfiguration : IEntityTypeConfiguration<FavoriteHut>
    {
        public void Configure(EntityTypeBuilder<FavoriteHut> builder)
        {
            builder.HasKey(fh => new { fh.UserId, fh.HutId });

            builder
                .HasOne(fh => fh.User)
                .WithMany(u => u.FavoriteHuts)
                .HasForeignKey(fh => fh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(fh => fh.Hut)
                .WithMany(h => h.FavoritedBy)
                .HasForeignKey(fh => fh.HutId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
