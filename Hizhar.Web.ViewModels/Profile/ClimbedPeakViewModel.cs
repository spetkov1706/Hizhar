namespace Hizhar.Web.ViewModels.Profile
{
    public class ClimbedPeakViewModel
    {
        public int PeakId { get; set; }

        public string Name { get; set; } = null!;

        public int Elevation { get; set; }

        public string MountainName { get; set; } = null!;

        public DateTime ClimbedOn { get; set; }
    }
}
