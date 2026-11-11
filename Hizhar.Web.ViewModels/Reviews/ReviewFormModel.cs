using System.ComponentModel.DataAnnotations;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Review;

namespace Hizhar.Web.ViewModels.Reviews
{
    public class ReviewFormModel
    {
        public int HutId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(RatingMin, RatingMax, ErrorMessage = RangeField)]
        [Display(Name = "Оценка")]
        public int Rating { get; set; } = 5;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Ревю")]
        public string Content { get; set; } = string.Empty;
    }
}
