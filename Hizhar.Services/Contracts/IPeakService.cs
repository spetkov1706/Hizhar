using Hizhar.Web.ViewModels.Peaks;

namespace Hizhar.Services.Contracts
{
    public interface IPeakService
    {
        Task<IEnumerable<PeakListItemViewModel>> GetAllAsync(AllPeaksQueryModel query, string? userId);

        Task<PeakDetailsViewModel?> GetDetailsAsync(int id, string? userId);

        Task<bool> ExistsAsync(int id);

        Task<PeakFormModel?> GetForEditAsync(int id);

        Task<int> CreateAsync(PeakFormModel model);

        Task<bool> EditAsync(int id, PeakFormModel model);

        Task<bool> DeleteAsync(int id);
    }
}
