namespace Hizhar.Web.ViewModels.Profile
{
    public class BadgeViewModel
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Icon { get; set; } = "award";

        public bool IsEarned { get; set; }
    }
}
