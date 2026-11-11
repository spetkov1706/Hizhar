using Hizhar.Data.Models.Enums;

namespace Hizhar.Web.ViewModels.HutKeeper
{
    public class HutKeeperRequestViewModel
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = null!;

        public int HutId { get; set; }

        public string HutName { get; set; } = null!;

        public string Message { get; set; } = null!;

        public RequestStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
