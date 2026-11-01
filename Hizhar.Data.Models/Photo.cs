using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hizhar.Common.ValidationConstants.Photo;

namespace Hizhar.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FilePathMaxLength)]
        public string FilePath { get; set; } = null!;

        [MaxLength(CaptionMaxLength)]
        public string? Caption { get; set; }

        public DateTime UploadedOn { get; set; }

        public int HutId { get; set; }

        [ForeignKey(nameof(HutId))]
        public virtual Hut Hut { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
