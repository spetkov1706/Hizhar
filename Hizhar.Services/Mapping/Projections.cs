using System.Linq.Expressions;
using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;
using Hizhar.Web.ViewModels.Huts;
using Hizhar.Web.ViewModels.Peaks;
using Hizhar.Web.ViewModels.TrailConditions;
using Hizhar.Web.ViewModels.Trails;
namespace Hizhar.Services.Mapping
{
    internal static class Projections
    {
        public static readonly Expression<Func<Hut, HutCardViewModel>> HutCard = h => new HutCardViewModel
        {
            Id = h.Id,
            Name = h.Name,
            MountainName = h.Mountain.Name,
            Altitude = h.Altitude,
            BedsCount = h.BedsCount,
            PricePerNight = h.PricePerNight,
            ImageUrl = h.ImageUrl,
            HasWater = h.HasWater,
            HasElectricity = h.HasElectricity,
            HasFood = h.HasFood,
            ReviewsCount = h.Reviews.Count,
            AverageRating = h.Reviews.Any() ? h.Reviews.Average(r => r.Rating) : 0
        };

        public static readonly Expression<Func<Trail, TrailCardViewModel>> TrailCard = t => new TrailCardViewModel
        {
            Id = t.Id,
            Name = t.Name,
            MountainName = t.Mountain.Name,
            LengthKm = t.LengthKm,
            ElevationGain = t.ElevationGain,
            DurationMinutes = t.DurationMinutes,
            Difficulty = t.Difficulty,
            Season = t.Season,
            StartPoint = t.Points.OrderBy(p => p.Order).Select(p => p.Name).FirstOrDefault() ?? string.Empty,
            EndPoint = t.Points.OrderByDescending(p => p.Order).Select(p => p.Name).FirstOrDefault() ?? string.Empty,
            ReportsCount = t.Conditions.Count,
            LatestConditionType = t.Conditions
              .OrderByDescending(c => c.ReportedOn)
              .Select(c => (ConditionType?)c.ConditionType)
              .FirstOrDefault(),
            LatestConditionDate = t.Conditions
              .OrderByDescending(c => c.ReportedOn)
              .Select(c => (DateTime?)c.ReportedOn)
              .FirstOrDefault()
        };

        public static readonly Expression<Func<TrailCondition, TrailConditionViewModel>> TrailConditionItem = c => new TrailConditionViewModel
        {
            Id = c.Id,
            TrailId = c.TrailId,
            TrailName = c.Trail.Name,
            ConditionType = c.ConditionType,
            Description = c.Description,
            ReportedOn = c.ReportedOn,
            AuthorName = c.User.UserName ?? "Анонимен"
        };

        public static Expression<Func<Peak, PeakListItemViewModel>> PeakListItem(string? userId) => p => new PeakListItemViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Elevation = p.Elevation,
            MountainId = p.MountainId,
            MountainName = p.Mountain.Name,
            IsNationalTouristSite = p.IsNationalTouristSite,
            ClimbersCount = p.ClimbedBy.Count,
            IsClimbed = userId != null && p.ClimbedBy.Any(c => c.UserId == userId)
        };
    }
}
