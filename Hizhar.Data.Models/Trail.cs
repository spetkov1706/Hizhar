using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.ValidationConstants.Trail;

namespace Hizhar.Data.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public double LengthKm { get; set; }

        public int ElevationGain { get; set; }

        public int DurationMinutes { get; set; }

        public Difficulty Difficulty { get; set; }

        public Season Season { get; set; }

        public int MountainId { get; set; }

        [ForeignKey(nameof(MountainId))]
        public virtual Mountain Mountain { get; set; } = null!;

        public virtual ICollection<TrailPoint> Points { get; set; } = new HashSet<TrailPoint>();

        public virtual ICollection<TrailCondition> Conditions { get; set; } = new HashSet<TrailCondition>();
    }
}
