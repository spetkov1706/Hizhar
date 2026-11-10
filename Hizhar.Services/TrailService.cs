using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Web.ViewModels.TrailConditions;
using Hizhar.Web.ViewModels.Trails;
using Hizhar.Web.ViewModels.Validation;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class TrailService : ITrailService
    {
        private const int ConditionsOnDetailsPage = 10;

        private readonly HizharDbContext context;

        public TrailService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TrailCardViewModel>> GetAllAsync(AllTrailsQueryModel query)
        {
            IQueryable<Trail> trails = context.Trails.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                string term = query.SearchTerm.Trim().ToLower();
                trails = trails.Where(t => t.Name.ToLower().Contains(term)
                                           || t.Points.Any(p => p.Name.ToLower().Contains(term)));
            }

            if (query.MountainId.HasValue)
            {
                trails = trails.Where(t => t.MountainId == query.MountainId.Value);
            }

            if (query.Difficulty.HasValue)
            {
                trails = trails.Where(t => t.Difficulty == query.Difficulty.Value);
            }

            if (query.Season.HasValue)
            {
                trails = trails.Where(t => t.Season == query.Season.Value);
            }

            if (query.MaxLengthKm.HasValue)
            {
                trails = trails.Where(t => t.LengthKm <= query.MaxLengthKm.Value);
            }

            return await trails
                .OrderBy(t => t.Difficulty)
                .ThenBy(t => t.Name)
                .Select(Projections.TrailCard)
                .ToListAsync();
        }

        public async Task<TrailDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await context.Trails
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TrailDetailsViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    MountainId = t.MountainId,
                    MountainName = t.Mountain.Name,
                    LengthKm = t.LengthKm,
                    ElevationGain = t.ElevationGain,
                    DurationMinutes = t.DurationMinutes,
                    Difficulty = t.Difficulty,
                    Season = t.Season,
                    Points = t.Points
                        .OrderBy(p => p.Order)
                        .Select(p => new TrailPointViewModel
                        {
                            Order = p.Order,
                            Name = p.Name,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            PointType = p.PointType
                        })
                        .ToList(),
                    Conditions = t.Conditions
                        .OrderByDescending(c => c.ReportedOn)
                        .Take(ConditionsOnDetailsPage)
                        .Select(c => new TrailConditionViewModel
                        {
                            Id = c.Id,
                            TrailId = c.TrailId,
                            TrailName = t.Name,
                            ConditionType = c.ConditionType,
                            Description = c.Description,
                            ReportedOn = c.ReportedOn,
                            AuthorName = c.User.UserName ?? "Анонимен"
                        })
                        .ToList(),
                    NewCondition = new TrailConditionFormModel { TrailId = t.Id }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrailCardViewModel>> GetPopularAsync(int count)
        {
            return await context.Trails
                .AsNoTracking()
                .OrderByDescending(t => t.Conditions.Count)
                .ThenBy(t => t.Name)
                .Take(count)
                .Select(Projections.TrailCard)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Trails.AnyAsync(t => t.Id == id);
        }

        public async Task<TrailFormModel?> GetForEditAsync(int id)
        {
            Trail? trail = await context.Trails
                .AsNoTracking()
                .Include(t => t.Points)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trail == null)
            {
                return null;
            }

            IEnumerable<TrailPointViewModel> points = trail.Points.Select(p => new TrailPointViewModel
            {
                Order = p.Order,
                Name = p.Name,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                PointType = p.PointType
            });

            return new TrailFormModel
            {
                Name = trail.Name,
                Description = trail.Description,
                LengthKm = trail.LengthKm,
                ElevationGain = trail.ElevationGain,
                DurationMinutes = trail.DurationMinutes,
                Difficulty = trail.Difficulty,
                Season = trail.Season,
                MountainId = trail.MountainId,
                PointsText = TrailPointsParser.ToText(points)
            };
        }

        public async Task<int> CreateAsync(TrailFormModel model)
        {
            var trail = new Trail();
            MapFormToEntity(model, trail);
            AddPoints(model.PointsText, trail);

            await context.Trails.AddAsync(trail);
            await context.SaveChangesAsync();

            return trail.Id;
        }

        public async Task<bool> EditAsync(int id, TrailFormModel model)
        {
            Trail? trail = await context.Trails
                .Include(t => t.Points)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trail == null)
            {
                return false;
            }

            MapFormToEntity(model, trail);

            context.TrailPoints.RemoveRange(trail.Points.ToList());
            trail.Points.Clear();
            AddPoints(model.PointsText, trail);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Trail? trail = await context.Trails.FindAsync(id);
            if (trail == null)
            {
                return false;
            }

            context.Trails.Remove(trail);
            await context.SaveChangesAsync();

            return true;
        }

        private static void MapFormToEntity(TrailFormModel model, Trail trail)
        {
            trail.Name = model.Name.Trim();
            trail.Description = model.Description.Trim();
            trail.LengthKm = model.LengthKm;
            trail.ElevationGain = model.ElevationGain;
            trail.DurationMinutes = model.DurationMinutes;
            trail.Difficulty = model.Difficulty;
            trail.Season = model.Season;
            trail.MountainId = model.MountainId;
        }

        private static void AddPoints(string pointsText, Trail trail)
        {
            if (!TrailPointsParser.TryParse(pointsText, out List<TrailPointViewModel> points, out string? error))
            {
                throw new InvalidOperationException(error);
            }

            foreach (TrailPointViewModel point in points)
            {
                trail.Points.Add(new TrailPoint
                {
                    Order = point.Order,
                    Name = point.Name,
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    PointType = point.PointType
                });
            }
        }
    }
}
