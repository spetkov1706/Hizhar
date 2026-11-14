namespace Hizhar.Web.ViewModels.Map
{
    public class MapPointViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Elevation { get; set; }
    }
}
