using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HizharDbContext context;

        public ReviewService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<OperationResult> AddAsync(ReviewFormModel model, string userId)
        {
            bool hutExists = await context.Huts.AnyAsync(h => h.Id == model.HutId && !h.IsDeleted);
            if (!hutExists)
            {
                return OperationResult.Failure("Хижата не съществува.");
            }

            bool alreadyReviewed = await context.Reviews.AnyAsync(r => r.HutId == model.HutId && r.UserId == userId);
            if (alreadyReviewed)
            {
                return OperationResult.Failure("Вече сте написали ревю за тази хижа.");
            }

            var review = new Review
            {
                HutId = model.HutId,
                UserId = userId,
                Rating = model.Rating,
                Content = model.Content.Trim(),
                CreatedOn = DateTime.UtcNow
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<int?> DeleteAsync(int reviewId, string userId, bool isAdmin)
        {
            Review? review = await context.Reviews.FindAsync(reviewId);

            if (review == null || (!isAdmin && review.UserId != userId))
            {
                return null;
            }

            int hutId = review.HutId;

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();

            return hutId;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetLatestAsync(int count)
        {
            return await context.Reviews
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedOn)
                .Take(count)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    HutId = r.HutId,
                    HutName = r.Hut.Name,
                    AuthorId = r.UserId,
                    AuthorName = r.User.UserName ?? "Анонимен",
                    Rating = r.Rating,
                    Content = r.Content,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();
        }
    }
}
