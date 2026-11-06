namespace Hizhar.Web.ViewModels.Mountains
{
    public class MountainIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public int HutsCount { get; set; }

        public int PeaksCount { get; set; }

        public int TrailsCount { get; set; }

        public string? HighestPeakName { get; set; }

        public int? HighestElevation { get; set; }
    }
}
