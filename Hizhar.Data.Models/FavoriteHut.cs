using System.ComponentModel.DataAnnotations.Schema;

namespace Hizhar.Data.Models
{
    public class FavoriteHut
    {
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public int HutId { get; set; }

        [ForeignKey(nameof(HutId))]
        public virtual Hut Hut { get; set; } = null!;

        public DateTime AddedOn { get; set; }
    }
}
