using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Web.ViewModels.Peaks;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class PeakService : IPeakService
    {
        private const int NearbyHutsCount = 3;

        private readonly HizharDbContext context;

        public PeakService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PeakListItemViewModel>> GetAllAsync(AllPeaksQueryModel query, string? userId)
        {
            IQueryable<Peak> peaks = context.Peaks.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                string term = query.SearchTerm.Trim().ToLower();
                peaks = peaks.Where(p => p.Name.ToLower().Contains(term));
            }

            if (query.MountainId.HasValue)
            {
                peaks = peaks.Where(p => p.MountainId == query.MountainId.Value);
            }

            if (query.MinElevation.HasValue)
            {
                peaks = peaks.Where(p => p.Elevation >= query.MinElevation.Value);
            }

            if (query.OnlyNational)
            {
                peaks = peaks.Where(p => p.IsNationalTouristSite);
            }

            return await peaks
                .OrderByDescending(p => p.Elevation)
                .Select(Projections.PeakListItem(userId))
                .ToListAsync();
        }

        public async Task<PeakDetailsViewModel?> GetDetailsAsync(int id, string? userId)
        {
            PeakDetailsViewModel? model = await context.Peaks
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PeakDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Elevation = p.Elevation,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Description = p.Description,
                    MountainId = p.MountainId,
                    MountainName = p.Mountain.Name,
                    IsNationalTouristSite = p.IsNationalTouristSite,
                    ClimbersCount = p.ClimbedBy.Count,
                    IsClimbed = userId != null && p.ClimbedBy.Any(c => c.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null;
            }

            var huts = await context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted && h.MountainId == model.MountainId)
                .Select(h => new { h.Id, h.Name, h.Altitude, h.Latitude, h.Longitude })
                .ToListAsync();

            model.NearbyHuts = huts
                .Select(h => new NearbyHutViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Altitude = h.Altitude,
                    DistanceKm = Math.Round(GeoHelper.DistanceKm(model.Latitude, model.Longitude, h.Latitude, h.Longitude), 1)
                })
                .OrderBy(h => h.DistanceKm)
                .Take(NearbyHutsCount)
                .ToList();

            return model;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Peaks.AnyAsync(p => p.Id == id);
        }

        public async Task<PeakFormModel?> GetForEditAsync(int id)
        {
            return await context.Peaks
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PeakFormModel
                {
                    Name = p.Name,
                    Elevation = p.Elevation,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Description = p.Description,
                    IsNationalTouristSite = p.IsNationalTouristSite,
                    MountainId = p.MountainId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(PeakFormModel model)
        {
            var peak = new Peak();
            MapFormToEntity(model, peak);

            await context.Peaks.AddAsync(peak);
            await context.SaveChangesAsync();

            return peak.Id;
        }

        public async Task<bool> EditAsync(int id, PeakFormModel model)
        {
            Peak? peak = await context.Peaks.FindAsync(id);
            if (peak == null)
            {
                return false;
            }

            MapFormToEntity(model, peak);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Peak? peak = await context.Peaks.FindAsync(id);
            if (peak == null)
            {
                return false;
            }

            context.Peaks.Remove(peak);
            await context.SaveChangesAsync();

            return true;
        }

        private static void MapFormToEntity(PeakFormModel model, Peak peak)
        {
            peak.Name = model.Name.Trim();
            peak.Elevation = model.Elevation;
            peak.Latitude = model.Latitude;
            peak.Longitude = model.Longitude;
            peak.Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim();
            peak.IsNationalTouristSite = model.IsNationalTouristSite;
            peak.MountainId = model.MountainId;
        }
    }
}
