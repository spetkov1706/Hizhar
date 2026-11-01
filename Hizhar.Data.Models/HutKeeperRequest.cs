using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.ValidationConstants.HutKeeperRequest;

namespace Hizhar.Data.Models
{
    public class HutKeeperRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; } = null!;

        public RequestStatus Status { get; set; }

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
