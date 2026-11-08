using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Hizhar.Web.ViewModels.Mountains;

namespace Hizhar.Web.ViewModels.Huts
{
    public class AllHutsQueryModel
    {
        public const int HutsPerPage = 6;

        [Display(Name = "Търсене")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Планина")]
        public int? MountainId { get; set; }

        [Display(Name = "Мин. надморска височина")]
        public int? MinBMinAltitude { get; set; }

        [Display(Name = "Мин. брой легла")]
        public int? MinBeds { get; set; }

        [Display(Name = "Има вода")]
        public bool HasWater { get; set; }

        [Display(Name = "Има ток")]
        public bool HasElectricity { get; set; }

        [Display(Name = "Има храна")]
        public bool HasFood { get; set; }

        [Display(Name = "Подреди по")]
        public HutSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalHuts { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalHuts / (double)HutsPerPage);

        public IEnumerable<HutCardViewModel> Huts { get; set; } = new List<HutCardViewModel>();

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
