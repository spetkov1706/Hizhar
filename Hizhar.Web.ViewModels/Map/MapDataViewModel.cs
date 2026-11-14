namespace Hizhar.Web.ViewModels.Map
{
    public class MapDataViewModel
    {
        public IEnumerable<MapPointViewModel> Huts { get; set; } = new List<MapPointViewModel>();

        public IEnumerable<MapPointViewModel> Peaks { get; set; } = new List<MapPointViewModel>();

        public IEnumerable<MapTrailViewModel> Trails { get; set; } = new List<MapTrailViewModel>();
    }
}
