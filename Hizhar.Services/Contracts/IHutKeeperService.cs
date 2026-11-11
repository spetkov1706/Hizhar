using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.HutKeeper;

namespace Hizhar.Services.Contracts
{
    public interface IHutKeeperService
    {
        Task<bool> HasPendingRequestAsync(string userId);

        Task<OperationResult> CreateRequestAsync(HutKeeperRequestFormModel model, string userId);

        Task<IEnumerable<HutKeeperRequestViewModel>> GetRequestsAsync();

        Task<int> GetPendingRequestsCountAsync();

        Task<OperationResult> ApproveRequestAsync(int requestId);

        Task<OperationResult> RejectRequestAsync(int requestId);


        Task<HutKeeperDashboardViewModel?> GetDashboardAsync(string keeperId);

        Task<HutKeeperHutFormModel?> GetHutForEditAsync(string keeperId);

        Task<bool> EditHutAsync(string keeperId, HutKeeperHutFormModel model);
    }
}
