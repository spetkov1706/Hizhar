using Hizhar.Data;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Map;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class MapService : IMapService
    {
        private readonly HizharDbContext context;

        public MapService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<MapDataViewModel> GetMapDataAsync()
        {
            List<MapPointViewModel> huts = await context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted)
                .Select(h => new MapPointViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Latitude = h.Latitude,
                    Longitude = h.Longitude,
                    Elevation = h.Altitude
                })
                .ToListAsync();

            List<MapPointViewModel> peaks = await context.Peaks
                .AsNoTracking()
                .Select(p => new MapPointViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Elevation = p.Elevation
                })
                .ToListAsync();

            var trails = await context.Trails
                .AsNoTracking()
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Difficulty,
                    Points = t.Points
                        .OrderBy(p => p.Order)
                        .Select(p => new { p.Latitude, p.Longitude })
                        .ToList()
                })
                .ToListAsync();

            return new MapDataViewModel
            {
                Huts = huts,
                Peaks = peaks,
                Trails = trails.Select(t => new MapTrailViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Difficulty = t.Difficulty.ToString(),
                    Coordinates = t.Points.Select(p => new[] { p.Latitude, p.Longitude }).ToList()
                })
                .ToList()
            };
        }
    }
}
