using System.ComponentModel.DataAnnotations;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Mountain;

namespace Hizhar.Web.ViewModels.Mountains
{
    public class MountainFormModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Url(ErrorMessage = InvalidUrl)]
        [StringLength(ImageUrlMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Снимка (URL)")]
        public string? ImageUrl { get; set; }
    }
}
