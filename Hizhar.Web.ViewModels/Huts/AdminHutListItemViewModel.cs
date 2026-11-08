namespace Hizhar.Web.ViewModels.Huts
{
    public class AdminHutListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string MountainName { get; set; } = null!;

        public int Altitude { get; set; }

        public int BedsCount { get; set; }

        public string? OwnerEmail { get; set; }
    }
}
