using Hizhar.Web.ViewModels.Huts;

namespace Hizhar.Services.Contracts
{
    public interface IHutService
    {
        Task<HutQueryResultViewModel> GetAllAsync(AllHutsQueryModel query);

        Task<HutDetailsViewModel?> GetDetailsAsync(int id, string? userId);

        Task<IEnumerable<HutCardViewModel>> GetTopRatedAsync(int count);

        Task<IEnumerable<HutOptionViewModel>> GetWithoutKeeperAsync();

        Task<bool> ExistsAsync(int id);

        Task<(double Latitude, double Longitude)?> GetLocationAsync(int id);


        Task<IEnumerable<AdminHutListItemViewModel>> GetAllForAdminAsync();

        Task<HutFormModel?> GetForEditAsync(int id);

        Task<int> CreateAsync(HutFormModel model);

        Task<bool> EditAsync(int id, HutFormModel model);

        Task<HutDeleteViewModel?> GetForDeleteAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
