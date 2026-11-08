namespace Hizhar.Web.ViewModels.Huts
{
    public class HutQueryResultViewModel
    {
        public int TotalCount { get; set; }

        public IEnumerable<HutCardViewModel> Huts { get; set; } = new List<HutCardViewModel>();
    }
}
