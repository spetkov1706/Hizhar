using System.ComponentModel.DataAnnotations;
using Hizhar.Data.Models.Enums;
using Hizhar.Web.ViewModels.Mountains;

namespace Hizhar.Web.ViewModels.Trails
{
    public class AllTrailsQueryModel
    {
        [Display(Name = "Търсене")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Планина")]
        public int? MountainId { get; set; }

        [Display(Name = "Трудност")]
        public Difficulty? Difficulty { get; set; }

        [Display(Name = "Сезон")]
        public Season? Season { get; set; }

        [Display(Name = "Макс. дължина (км)")]
        public double? MaxLengthKm { get; set; }

        public IEnumerable<TrailCardViewModel> Trails { get; set; } = new List<TrailCardViewModel>();

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
