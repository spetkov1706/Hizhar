using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hizhar.Common.ValidationConstants.Review;

namespace Hizhar.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int HutId { get; set; }

        [ForeignKey(nameof(HutId))]
        public virtual Hut Hut { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
