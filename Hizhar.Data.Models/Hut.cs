using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hizhar.Common.ValidationConstants.Hut;

namespace Hizhar.Data.Models
{
    public class Hut
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int Altitude { get; set; }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public int BedsCount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }
        
        public bool HasWater { get; set; }
        
        public bool HasElectricity { get; set; }
        
        public bool HasFood { get; set; }
        
        [MaxLength(PhoneNumberMaxLength)]
        public string? PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }
        
        public bool IsDeleted { get; set; }

        public int MountainId { get; set; }

        [ForeignKey(nameof(MountainId))]
        public virtual Mountain Mountain { get; set; } = null!;

        public string? OwnerId { get; set; }

        public virtual ApplicationUser? Owner { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<FavoriteHut> FavoritedBy { get; set; } = new HashSet<FavoriteHut>();

        public virtual ICollection<HutKeeperRequest> KeeperRequests { get; set; } = new HashSet<HutKeeperRequest>();
    }
}
