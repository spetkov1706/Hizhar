using System.ComponentModel.DataAnnotations;
using Hizhar.Data.Models.Enums;
using Hizhar.Web.ViewModels.Mountains;
using Hizhar.Web.ViewModels.Validation;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Trail;

namespace Hizhar.Web.ViewModels.Trails
{
    public class TrailFormModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Range(LengthMin, LengthMax, ErrorMessage = RangeField)]
        [Display(Name = "Дължина (км)")]
        public double LengthKm { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(ElevationGainMin, ElevationGainMax, ErrorMessage = RangeField)]
        [Display(Name = "Денивелация (м)")]
        public int ElevationGain { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(DurationMin, DurationMax, ErrorMessage = RangeField)]
        [Display(Name = "Време (минути)")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [EnumDataType(typeof(Difficulty), ErrorMessage = SelectRequired)]
        [Display(Name = "Трудност")]
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        [Required(ErrorMessage = RequiredField)]
        [EnumDataType(typeof(Season), ErrorMessage = SelectRequired)]
        [Display(Name = "Сезон")]
        public Season Season { get; set; } = Season.AllYear;

        [Range(1, int.MaxValue, ErrorMessage = SelectRequired)]
        [Display(Name = "Планина")]
        public int MountainId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [StringLength(PointsTextMaxLength, ErrorMessage = MaxLengthField)]
        [ValidTrailPoints]
        [Display(Name = "Точки на маршрута")]
        public string PointsText { get; set; } = string.Empty;

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
