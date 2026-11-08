namespace Hizhar.Web.ViewModels.Peaks
{
    public class PeakDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Elevation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Description { get; set; }

        public int MountainId { get; set; }

        public string MountainName { get; set; } = null!;

        public bool IsNationalTouristSite { get; set; }

        public int ClimbersCount { get; set; }

        public bool IsClimbed { get; set; }

        public IEnumerable<NearbyHutViewModel> NearbyHuts { get; set; } = new List<NearbyHutViewModel>();
    }
}
