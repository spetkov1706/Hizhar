using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Reviews;

namespace Hizhar.Services.Contracts
{
    public interface IReviewService
    {
        Task<OperationResult> AddAsync(ReviewFormModel model, string userId);

        Task<int?> DeleteAsync(int reviewId, string userId, bool isAdmin);

        Task<IEnumerable<ReviewViewModel>> GetLatestAsync(int count);
    }
}
