using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hizhar.Common.ValidationConstants.Peak;

namespace Hizhar.Data.Models
{
    public class Peak
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public int Elevation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        public bool IsNationalTouristSite { get; set; }

        public int MountainId { get; set; }

        [ForeignKey(nameof(MountainId))]
        public virtual Mountain Mountain { get; set; } = null!;

        public virtual ICollection<ClimbedPeak> ClimbedBy { get; set; } = new HashSet<ClimbedPeak>();
    }
}
