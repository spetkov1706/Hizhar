namespace Hizhar.Web.ViewModels.Peaks
{
    public class NearbyHutViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Altitude { get; set; }

        public double DistanceKm { get; set; }
    }
}
