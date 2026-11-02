using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hizhar.Data.Models;

namespace Hizhar.Data
{
    public class HizharDbContext : IdentityDbContext<ApplicationUser>
    {
        public HizharDbContext(DbContextOptions<HizharDbContext> options) : base(options)
        {
        }

        public DbSet<Mountain> Mountains { get; set; } = null!;

        public DbSet<Hut> Huts { get; set; } = null!;

        public DbSet<Peak> Peaks { get; set; } = null!;

        public DbSet<Trail> Trails { get; set; } = null!;

        public DbSet<TrailPoint> TrailPoints { get; set; } = null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Photo> Photos { get; set; } = null!;

        public DbSet<TrailCondition> TrailConditions { get; set; } = null!;

        public DbSet<ClimbedPeak> ClimbedPeaks { get; set; } = null!;

        public DbSet<FavoriteHut> FavoriteHuts { get; set; } = null!;

        public DbSet<HutKeeperRequest> HutKeeperRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(HizharDbContext).Assembly);
        }
    }
}
