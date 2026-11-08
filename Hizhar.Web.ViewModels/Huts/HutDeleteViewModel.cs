namespace Hizhar.Web.ViewModels.Huts
{
    public class HutDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string MountainName { get; set; } = null!;

        public int ActiveReservationsCount { get; set; }
    }
}
