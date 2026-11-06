using Hizhar.Web.ViewModels.Huts;
using Hizhar.Web.ViewModels.Peaks;
using Hizhar.Web.ViewModels.Trails;

namespace Hizhar.Web.ViewModels.Mountains
{
    public class MountainDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public IEnumerable<HutCardViewModel> Huts { get; set; } = new List<HutCardViewModel>();

        public IEnumerable<PeakListItemViewModel> Peaks { get; set; } = new List<PeakListItemViewModel>();

        public IEnumerable<TrailCardViewModel> Trails { get; set; } = new List<TrailCardViewModel>();
    }
}
