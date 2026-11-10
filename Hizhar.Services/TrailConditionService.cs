using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Web.ViewModels.TrailConditions;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class TrailConditionService : ITrailConditionService
    {
        private readonly HizharDbContext context;

        public TrailConditionService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TrailConditionFormModel model, string userId)
        {
            var condition = new TrailCondition
            {
                TrailId = model.TrailId,
                UserId = userId,
                ConditionType = model.ConditionType,
                Description = model.Description.Trim(),
                ReportedOn = DateTime.UtcNow
            };

            await context.TrailConditions.AddAsync(condition);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrailConditionViewModel>> GetLatestAsync(int count)
        {
            return await context.TrailConditions
                .AsNoTracking()
                .OrderByDescending(c => c.ReportedOn)
                .Take(count)
                .Select(Projections.TrailConditionItem)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TrailCondition? condition = await context.TrailConditions.FindAsync(id);
            if (condition == null)
            {
                return false;
            }

            context.TrailConditions.Remove(condition);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
