using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Photos;
using Microsoft.AspNetCore.Http;

namespace Hizhar.Services.Contracts
{
    public interface IPhotoService
    {
        Task<OperationResult> AddAsync(int hutId, string userId, IFormFile file, string? caption);

        Task<int?> DeleteAsync(int photoId, string userId, bool isAdmin);

        Task<IEnumerable<PhotoViewModel>> GetLatestAsync(int count);
    }
}
