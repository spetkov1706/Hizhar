using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.ValidationConstants.TrailPoint;

namespace Hizhar.Data.Models
{
    public class TrailPoint
    {
        [Key]
        public int Id { get; set; }

        public int Order { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TrailPointType PointType { get; set; }

        public int TrailId { get; set; }

        [ForeignKey(nameof(TrailId))]
        public virtual Trail Trail { get; set; } = null!;
    }
}
