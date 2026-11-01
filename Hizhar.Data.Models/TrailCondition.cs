using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.ValidationConstants.TrailCondition;

namespace Hizhar.Data.Models
{
    public class TrailCondition
    {
        [Key]
        public int Id { get; set; }

        public ConditionType ConditionType { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime ReportedOn { get; set; }

        public int TrailId { get; set; }

        [ForeignKey(nameof(TrailId))]
        public virtual Trail Trail { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
