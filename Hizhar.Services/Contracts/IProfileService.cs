using Hizhar.Web.ViewModels.Profile;

namespace Hizhar.Services.Contracts
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetProfileAsync(string userId);
    }
}
