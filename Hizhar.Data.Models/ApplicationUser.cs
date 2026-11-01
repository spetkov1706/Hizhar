using Hizhar.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using static Hizhar.Common.ValidationConstants;

namespace Hizhar.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Hut> OwnedHuts { get; set; } = new HashSet<Hut>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<TrailCondition> TrailConditions { get; set; } = new HashSet<TrailCondition>();

        public virtual ICollection<ClimbedPeak> ClimbedPeaks { get; set; } = new HashSet<ClimbedPeak>();

        public virtual ICollection<FavoriteHut> FavoriteHuts { get; set; } = new HashSet<FavoriteHut>();

        public virtual ICollection<HutKeeperRequest> HutKeeperRequests { get; set; } = new HashSet<HutKeeperRequest>();
    }
}
