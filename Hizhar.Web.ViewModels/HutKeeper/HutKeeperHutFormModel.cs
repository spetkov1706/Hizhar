using System.ComponentModel.DataAnnotations;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Hut;

namespace Hizhar.Web.ViewModels.HutKeeper
{
    public class HutKeeperHutFormModel
    {
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Range(BedsMin, BedsMax, ErrorMessage = RangeField)]
        [Display(Name = "Брой легла")]
        public int BedsCount { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(typeof(decimal), PriceMin, PriceMax, ErrorMessage = RangeField)]
        [Display(Name = "Цена на нощувка (лв.)")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Има течаща вода")]
        public bool HasWater { get; set; }

        [Display(Name = "Има ток")]
        public bool HasElectricity { get; set; }

        [Display(Name = "Предлага храна")]
        public bool HasFood { get; set; }

        [RegularExpression(PhoneNumberRegex, ErrorMessage = InvalidPhone)]
        [StringLength(PhoneNumberMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [Url(ErrorMessage = InvalidUrl)]
        [StringLength(ImageUrlMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Снимка (URL)")]
        public string? ImageUrl { get; set; }
    }
}
