using Hizhar.Web.ViewModels.Mountains;
using System.ComponentModel.DataAnnotations;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Coordinates;
using static Hizhar.Common.ValidationConstants.Hut;

namespace Hizhar.Web.ViewModels.Huts
{
    public class HutFormModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = SelectRequired)]
        [Display(Name = "Планина")]
        public int MountainId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(AltitudeMin, AltitudeMax, ErrorMessage = RangeField)]
        [Display(Name = "Надморска височина (м)")]
        public int Altitude { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(LatitudeMin, LatitudeMax, ErrorMessage = RangeField)]
        [Display(Name = "Географска ширина")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(LongitudeMin, LongitudeMax, ErrorMessage = RangeField)]
        [Display(Name = "Географска дължина")]
        public double Longitude { get; set; }

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

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
