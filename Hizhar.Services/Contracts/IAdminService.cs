using Hizhar.Web.ViewModels.Admin;

namespace Hizhar.Services.Contracts
{
    public interface IAdminService
    {
        Task<AdminDashboardViewModel> GetDashboardAsync();

        Task<IEnumerable<UserListItemViewModel>> GetUsersAsync();
    }
}
