namespace Hizhar.Web.ViewModels.Map
{
    public class MapTrailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Difficulty { get; set; } = null!;

        public IEnumerable<double[]> Coordinates { get; set; } = new List<double[]>();
    }
}
