using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Photos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Hizhar.Common.ValidationConstants.Photo;

namespace Hizhar.Services
{
    public class PhotoService : IPhotoService
    {
        private const string UploadFolder = "uploads/huts";

        private readonly HizharDbContext context;
        private readonly IWebHostEnvironment environment;

        public PhotoService(HizharDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public async Task<OperationResult> AddAsync(int hutId, string userId, IFormFile file, string? caption)
        {
            bool hutExists = await context.Huts.AnyAsync(h => h.Id == hutId && !h.IsDeleted);
            if (!hutExists)
            {
                return OperationResult.Failure("Хижата не съществува.");
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string[] allowed = AllowedExtensions.Split(',');

            if (!allowed.Contains(extension))
            {
                return OperationResult.Failure("Неподдържан формат на файла.");
            }

            if (file.Length == 0 || file.Length > MaxFileSizeInBytes)
            {
                return OperationResult.Failure("Файлът е празен или твърде голям.");
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string folderPath = Path.Combine(environment.WebRootPath, UploadFolder);
            Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo
            {
                HutId = hutId,
                UserId = userId,
                FilePath = $"/{UploadFolder}/{fileName}",
                Caption = string.IsNullOrWhiteSpace(caption) ? null : caption.Trim(),
                UploadedOn = DateTime.UtcNow
            };

            await context.Photos.AddAsync(photo);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<int?> DeleteAsync(int photoId, string userId, bool isAdmin)
        {
            Photo? photo = await context.Photos.FindAsync(photoId);

            if (photo == null || (!isAdmin && photo.UserId != userId))
            {
                return null;
            }

            string fullPath = Path.Combine(environment.WebRootPath, photo.FilePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            int hutId = photo.HutId;

            context.Photos.Remove(photo);
            await context.SaveChangesAsync();

            return hutId;
        }

        public async Task<IEnumerable<PhotoViewModel>> GetLatestAsync(int count)
        {
            return await context.Photos
                .AsNoTracking()
                .OrderByDescending(p => p.UploadedOn)
                .Take(count)
                .Select(p => new PhotoViewModel
                {
                    Id = p.Id,
                    HutId = p.HutId,
                    HutName = p.Hut.Name,
                    FilePath = p.FilePath,
                    Caption = p.Caption,
                    AuthorId = p.UserId,
                    AuthorName = p.User.UserName ?? "Анонимен",
                    UploadedOn = p.UploadedOn
                })
                .ToListAsync();
        }
    }
}
