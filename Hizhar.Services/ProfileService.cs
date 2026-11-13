using Hizhar.Data;
using Hizhar.Data.Models.Enums;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Web.ViewModels.Profile;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class ProfileService : IProfileService
    {
        private const int HighPeakThreshold = 2500;

        private readonly HizharDbContext context;

        public ProfileService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<ProfileViewModel> GetProfileAsync(string userId)
        {
            string email = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefaultAsync() ?? string.Empty;

            var peaks = await context.Peaks
                .AsNoTracking()
                .Select(p => new
                {
                    p.Elevation,
                    p.IsNationalTouristSite,
                    MountainName = p.Mountain.Name,
                    IsClimbed = p.ClimbedBy.Any(c => c.UserId == userId)
                })
                .ToListAsync();

            List<ClimbedPeakViewModel> climbedPeaks = await context.ClimbedPeaks
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.ClimbedOn)
                .Select(c => new ClimbedPeakViewModel
                {
                    PeakId = c.PeakId,
                    Name = c.Peak.Name,
                    Elevation = c.Peak.Elevation,
                    MountainName = c.Peak.Mountain.Name,
                    ClimbedOn = c.ClimbedOn
                })
                .ToListAsync();

            var favoriteHuts = await context.FavoriteHuts
                .AsNoTracking()
                .Where(f => f.UserId == userId && !f.Hut.IsDeleted)
                .OrderByDescending(f => f.AddedOn)
                .Select(f => f.Hut)
                .Select(Projections.HutCard)
                .ToListAsync();

            int reservationsCount = await context.Reservations.CountAsync(r => r.UserId == userId);
            int reviewsCount = await context.Reviews.CountAsync(r => r.UserId == userId);
            int reportsCount = await context.TrailConditions.CountAsync(c => c.UserId == userId);

            DateTime today = DateTime.Today;
            int hutsVisitedThisYear = await context.Reservations
                .Where(r => r.UserId == userId
                            && r.Status == ReservationStatus.Confirmed
                            && r.CheckOut <= today
                            && r.CheckIn.Year == today.Year)
                .Select(r => r.HutId)
                .Distinct()
                .CountAsync();

            var model = new ProfileViewModel
            {
                Email = email,
                TotalPeaks = peaks.Count,
                ClimbedPeaksCount = peaks.Count(p => p.IsClimbed),
                PeaksAbove2500Total = peaks.Count(p => p.Elevation >= HighPeakThreshold),
                PeaksAbove2500Climbed = peaks.Count(p => p.Elevation >= HighPeakThreshold && p.IsClimbed),
                NationalSitesTotal = peaks.Count(p => p.IsNationalTouristSite),
                NationalSitesClimbed = peaks.Count(p => p.IsNationalTouristSite && p.IsClimbed),
                ReservationsCount = reservationsCount,
                ReviewsCount = reviewsCount,
                ReportsCount = reportsCount,
                ClimbedPeaks = climbedPeaks,
                FavoriteHuts = favoriteHuts
            };

            var badges = new List<BadgeViewModel>
            {
                new BadgeViewModel
                {
                    Title = "Първа стъпка",
                    Description = "Изкачете първия си връх.",
                    Icon = "flag",
                    IsEarned = model.ClimbedPeaksCount >= 1
                },
                new BadgeViewModel
                {
                    Title = "Над облаците",
                    Description = "Изкачете 3 върха над 2500 м.",
                    Icon = "cloud-sun",
                    IsEarned = model.PeaksAbove2500Climbed >= 3
                },
                new BadgeViewModel
                {
                    Title = "Национален турист",
                    Description = "Изкачете 5 върха от националните туристически обекти.",
                    Icon = "geo-alt",
                    IsEarned = model.NationalSitesClimbed >= 5
                },
                new BadgeViewModel
                {
                    Title = "10 хижи за сезона",
                    Description = "Пренощувайте в 10 различни хижи през тази година.",
                    Icon = "house-heart",
                    IsEarned = hutsVisitedThisYear >= 10
                },
                new BadgeViewModel
                {
                    Title = "Репортер на пътеките",
                    Description = "Подайте 5 доклада за състоянието на пътеки.",
                    Icon = "megaphone",
                    IsEarned = reportsCount >= 5
                },
                new BadgeViewModel
                {
                    Title = "Хижарски критик",
                    Description = "Напишете 3 ревюта за хижи.",
                    Icon = "chat-quote",
                    IsEarned = reviewsCount >= 3
                }
            };

            var mountainBadges = peaks
                .GroupBy(p => p.MountainName)
                .OrderBy(g => g.Key)
                .Select(g => new BadgeViewModel
                {
                    Title = $"Покорител на {g.Key}",
                    Description = $"Изкачете всички {g.Count()} върха в {g.Key}.",
                    Icon = "trophy",
                    IsEarned = g.All(p => p.IsClimbed)
                });

            badges.AddRange(mountainBadges);
            model.Badges = badges;

            return model;
        }
    }
}
