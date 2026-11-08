using System.ComponentModel.DataAnnotations;
using Hizhar.Web.ViewModels.Mountains;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Coordinates;
using static Hizhar.Common.ValidationConstants.Peak;

namespace Hizhar.Web.ViewModels.Peaks
{
    public class PeakFormModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Range(ElevationMin, ElevationMax, ErrorMessage = RangeField)]
        [Display(Name = "Надморска височина (м)")]
        public int Elevation { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(LatitudeMin, LatitudeMax, ErrorMessage = RangeField)]
        [Display(Name = "Географска ширина")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(LongitudeMin, LongitudeMax, ErrorMessage = RangeField)]
        [Display(Name = "Географска дължина")]
        public double Longitude { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "От 100-те национални туристически обекта")]
        public bool IsNationalTouristSite { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = SelectRequired)]
        [Display(Name = "Планина")]
        public int MountainId { get; set; }

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
