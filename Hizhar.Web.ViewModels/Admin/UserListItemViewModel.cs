namespace Hizhar.Web.ViewModels.Admin
{
    public class UserListItemViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();

        public int ReservationsCount { get; set; }

        public int ReviewsCount { get; set; }

        public string? OwnedHutName { get; set; }
    }
}
