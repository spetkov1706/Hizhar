namespace Hizhar.Web.ViewModels.Peaks
{
    public class PeakListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Elevation { get; set; }

        public int MountainId { get; set; }

        public string MountainName { get; set; } = null!;

        public bool IsNationalTouristSite { get; set; }

        public int ClimbersCount { get; set; }

        public bool IsClimbed { get; set; }
    }
}
