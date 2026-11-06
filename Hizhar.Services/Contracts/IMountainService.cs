using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Mountains;

namespace Hizhar.Services.Contracts
{
    public interface IMountainService
    {
        Task<IEnumerable<MountainIndexViewModel>> GetAllAsync();

        Task<MountainDetailsViewModel?> GetDetailsAsync(int id, string? userId);

        Task<IEnumerable<MountainOptionViewModel>> GetOptionsAsync();

        Task<bool> ExistsAsync(int id);

        Task<bool> NameExistsAsync(string name, int? excludeId = null);

        Task<MountainFormModel?> GetForEditAsync(int id);

        Task<int> CreateAsync(MountainFormModel model);

        Task<bool> EditAsync(int id, MountainFormModel model);

        Task<OperationResult> DeleteAsync(int id);
    }
}
