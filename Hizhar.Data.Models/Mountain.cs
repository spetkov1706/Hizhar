using System.ComponentModel.DataAnnotations;
using static Hizhar.Common.ValidationConstants.Mountain;

namespace Hizhar.Data.Models
{
    public class Mountain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public  string? ImageUrl { get; set; }

        public virtual ICollection<Hut> Huts { get; set; } = new HashSet<Hut>();
        public virtual ICollection<Peak> Peaks { get; set; } = new HashSet<Peak>();
        public virtual ICollection<Trail> Trails { get; set; } = new HashSet<Trail>();

    }
}
