using Hizhar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hizhar.Data.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.CheckIn).HasColumnType("date");
            builder.Property(r => r.CheckOut).HasColumnType("date");

            builder.HasIndex(r => new { r.HutId, r.CheckIn, r.CheckOut });

            builder
                .HasOne(r => r.Hut)
                .WithMany(h => h.Reservations)
                .HasForeignKey(r => r.HutId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
