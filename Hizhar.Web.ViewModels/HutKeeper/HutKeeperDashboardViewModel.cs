using Hizhar.Web.ViewModels.Reservations;

namespace Hizhar.Web.ViewModels.HutKeeper
{
    public class HutKeeperDashboardViewModel
    {
        public int HutId { get; set; }

        public string HutName { get; set; } = null!;

        public int BedsCount { get; set; }

        public int FreeBedsTonight { get; set; }

        public IEnumerable<ReservationListItemViewModel> PendingReservations { get; set; } = new List<ReservationListItemViewModel>();

        public IEnumerable<ReservationListItemViewModel> UpcomingReservations { get; set; } = new List<ReservationListItemViewModel>();
    }
}
