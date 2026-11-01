using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.ValidationConstants.Reservation;

namespace Hizhar.Data.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Guests { get; set; }

        [MaxLength(NoteMaxLength)]
        public string? Note { get; set; }

        public ReservationStatus Status { get; set; }

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
