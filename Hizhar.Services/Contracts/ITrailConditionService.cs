using Hizhar.Web.ViewModels.TrailConditions;

namespace Hizhar.Services.Contracts
{
    public interface ITrailConditionService
    {
        Task AddAsync(TrailConditionFormModel model, string userId);

        Task<IEnumerable<TrailConditionViewModel>> GetLatestAsync(int count);

        Task<bool> DeleteAsync(int id);
    }
}
