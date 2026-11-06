using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Mountains;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class MountainService : IMountainService
    {
        private readonly HizharDbContext context;

        public MountainService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MountainIndexViewModel>> GetAllAsync()
        {
            return await context.Mountains
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new MountainIndexViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    HutsCount = m.Huts.Count(h => !h.IsDeleted),
                    PeaksCount = m.Peaks.Count,
                    TrailsCount = m.Trails.Count,
                    HighestPeakName = m.Peaks.OrderByDescending(p => p.Elevation).Select(p => p.Name).FirstOrDefault(),
                    HighestPeakElevation = m.Peaks.OrderByDescending(p => p.Elevation).Select(p => (int?)p.Elevation).FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<MountainDetailsViewModel?> GetDetailsAsync(int id, string? userId)
        {
            MountainDetailsViewModel? model = await context.Mountains
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new MountainDetailsViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null;
            }

            model.Huts = await context.Huts
                .AsNoTracking()
                .Where(h => h.MountainId == id && !h.IsDeleted)
                .OrderByDescending(h => h.Altitude)
                .Select(Projections.HutCard)
                .ToListAsync();

            model.Peaks = await context.Peaks
                .AsNoTracking()
                .Where(p => p.MountainId == id)
                .OrderByDescending(p => p.Elevation)
                .Select(Projections.PeakListItem(userId))
                .ToListAsync();

            model.Trails = await context.Trails
                .AsNoTracking()
                .Where(t => t.MountainId == id)
                .OrderBy(t => t.Name)
                .Select(Projections.TrailCard)
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<MountainOptionViewModel>> GetOptionsAsync()
        {
            return await context.Mountains
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new MountainOptionViewModel { Id = m.Id, Name = m.Name })
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Mountains.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            string normalized = name.Trim().ToLower();

            return await context.Mountains
                .AnyAsync(m => m.Name.ToLower() == normalized && (excludeId == null || m.Id != excludeId));
        }

        public async Task<MountainFormModel?> GetForEditAsync(int id)
        {
            return await context.Mountains
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new MountainFormModel
                {
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(MountainFormModel model)
        {
            var mountain = new Mountain
            {
                Name = model.Name.Trim(),
                Description = model.Description.Trim(),
                ImageUrl = model.ImageUrl
            };

            await context.Mountains.AddAsync(mountain);
            await context.SaveChangesAsync();

            return mountain.Id;
        }

        public async Task<bool> EditAsync(int id, MountainFormModel model)
        {
            Mountain? mountain = await context.Mountains.FindAsync(id);
            if (mountain == null)
            {
                return false;
            }

            mountain.Name = model.Name.Trim();
            mountain.Description = model.Description.Trim();
            mountain.ImageUrl = model.ImageUrl;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            Mountain? mountain = await context.Mountains
                .Include(m => m.Huts)
                .Include(m => m.Peaks)
                .Include(m => m.Trails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mountain == null)
            {
                return OperationResult.Failure("Планината не съществува.");
            }

            if (mountain.Huts.Any() || mountain.Peaks.Any() || mountain.Trails.Any())
            {
                return OperationResult.Failure("Планината не може да бъде изтрита, докато в нея има хижи, върхове или маршрути.");
            }

            context.Mountains.Remove(mountain);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }
    }
}
