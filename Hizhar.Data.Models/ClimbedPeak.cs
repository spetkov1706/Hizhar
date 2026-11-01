using System.ComponentModel.DataAnnotations.Schema;

namespace Hizhar.Data.Models
{
    public class ClimbedPeak
    {
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public int PeakId { get; set; }

        [ForeignKey(nameof(PeakId))]
        public virtual Peak Peak { get; set; } = null!;

        public DateTime ClimbedOn { get; set; }
    }
}
