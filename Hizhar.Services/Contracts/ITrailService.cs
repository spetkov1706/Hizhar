using Hizhar.Web.ViewModels.Trails;

namespace Hizhar.Services.Contracts
{
    public interface ITrailService
    {
        Task<IEnumerable<TrailCardViewModel>> GetAllAsync(AllTrailsQueryModel query);

        Task<TrailDetailsViewModel?> GetDetailsAsync(int id);

        Task<IEnumerable<TrailCardViewModel>> GetPopularAsync(int count);

        Task<bool> ExistsAsync(int id);


        Task<TrailFormModel?> GetForEditAsync(int id);

        Task<int> CreateAsync(TrailFormModel model);

        Task<bool> EditAsync(int id, TrailFormModel model);

        Task<bool> DeleteAsync(int id);
    }
}
