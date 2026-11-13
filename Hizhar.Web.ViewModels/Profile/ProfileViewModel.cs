using Hizhar.Web.ViewModels.Huts;

namespace Hizhar.Web.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public string Email { get; set; } = null!;

        public int TotalPeaks { get; set; }

        public int ClimbedPeaksCount { get; set; }

        public int PeaksAbove2500Total { get; set; }

        public int PeaksAbove2500Climbed { get; set; }

        public int NationalSitesTotal { get; set; }

        public int NationalSitesClimbed { get; set; }

        public int ReservationsCount { get; set; }

        public int ReviewsCount { get; set; }

        public int ReportsCount { get; set; }

        public IEnumerable<ClimbedPeakViewModel> ClimbedPeaks { get; set; } = new List<ClimbedPeakViewModel>();

        public IEnumerable<HutCardViewModel> FavoriteHuts { get; set; } = new List<HutCardViewModel>();

        public IEnumerable<BadgeViewModel> Badges { get; set; } = new List<BadgeViewModel>();

        public int NationalProgressPercent => NationalSitesTotal == 0
            ? 0
            : (int)Math.Round(NationalSitesClimbed * 100.0 / NationalSitesTotal);

        public int AllPeaksProgressPercent => TotalPeaks == 0
            ? 0
            : (int)Math.Round(ClimbedPeaksCount * 100.0 / TotalPeaks);
    }
}
