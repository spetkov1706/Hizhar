using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;
using Hizhar.Services.Contracts;
using Hizhar.Services.Mapping;
using Hizhar.Web.ViewModels.Huts;
using Hizhar.Web.ViewModels.Photos;
using Hizhar.Web.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class HutService : IHutService
    {
        private readonly HizharDbContext context;

        public HutService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<HutQueryResultViewModel> GetAllAsync(AllHutsQueryModel query)
        {
            IQueryable<Hut> huts = context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                string term = query.SearchTerm.Trim().ToLower();
                huts = huts.Where(h => h.Name.ToLower().Contains(term) || h.Description.ToLower().Contains(term));
            }

            if (query.MountainId.HasValue)
            {
                huts = huts.Where(h => h.MountainId == query.MountainId.Value);
            }

            if (query.MinAltitude.HasValue)
            {
                huts = huts.Where(h => h.Altitude >= query.MinAltitude.Value);
            }

            if (query.MinBeds.HasValue)
            {
                huts = huts.Where(h => h.BedsCount >= query.MinBeds.Value);
            }

            if (query.HasWater)
            {
                huts = huts.Where(h => h.HasWater);
            }

            if (query.HasElectricity)
            {
                huts = huts.Where(h => h.HasElectricity);
            }

            if (query.HasFood)
            {
                huts = huts.Where(h => h.HasFood);
            }

            huts = query.Sorting switch
            {
                HutSorting.Altitude => huts.OrderByDescending(h => h.Altitude),
                HutSorting.Beds => huts.OrderByDescending(h => h.BedsCount),
                HutSorting.Price => huts.OrderBy(h => h.PricePerNight),
                HutSorting.Name => huts.OrderBy(h => h.Name),
                _ => huts
                    .OrderByDescending(h => h.Reviews.Any() ? h.Reviews.Average(r => r.Rating) : 0)
                    .ThenBy(h => h.Name)
            };

            int totalCount = await huts.CountAsync();

            int page = Math.Max(1, query.CurrentPage);

            List<HutCardViewModel> pageItems = await huts
                .Skip((page - 1) * AllHutsQueryModel.HutsPerPage)
                .Take(AllHutsQueryModel.HutsPerPage)
                .Select(Projections.HutCard)
                .ToListAsync();

            return new HutQueryResultViewModel
            {
                TotalCount = totalCount,
                Huts = pageItems
            };
        }

        public async Task<HutDetailsViewModel?> GetDetailsAsync(int id, string? userId)
        {
            HutDetailsViewModel? model = await context.Huts
                .AsNoTracking()
                .Where(h => h.Id == id && !h.IsDeleted)
                .Select(h => new HutDetailsViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    MountainId = h.MountainId,
                    MountainName = h.Mountain.Name,
                    Altitude = h.Altitude,
                    Latitude = h.Latitude,
                    Longitude = h.Longitude,
                    BedsCount = h.BedsCount,
                    PricePerNight = h.PricePerNight,
                    HasWater = h.HasWater,
                    HasElectricity = h.HasElectricity,
                    HasFood = h.HasFood,
                    PhoneNumber = h.PhoneNumber,
                    ImageUrl = h.ImageUrl,
                    KeeperEmail = h.Owner != null ? h.Owner.Email : null,
                    ReviewsCount = h.Reviews.Count,
                    AverageRating = h.Reviews.Any() ? h.Reviews.Average(r => r.Rating) : 0,
                    IsFavorite = userId != null && h.FavoritedBy.Any(f => f.UserId == userId),
                    HasReviewed = userId != null && h.Reviews.Any(r => r.UserId == userId),
                    Reviews = h.Reviews
                        .OrderByDescending(r => r.CreatedOn)
                        .Select(r => new ReviewViewModel
                        {
                            Id = r.Id,
                            HutId = r.HutId,
                            AuthorId = r.UserId,
                            AuthorName = r.User.UserName ?? "Анонимен",
                            Rating = r.Rating,
                            Content = r.Content,
                            CreatedOn = r.CreatedOn
                        })
                        .ToList(),
                    Photos = h.Photos
                        .OrderByDescending(p => p.UploadedOn)
                        .Select(p => new PhotoViewModel
                        {
                            Id = p.Id,
                            HutId = p.HutId,
                            FilePath = p.FilePath,
                            Caption = p.Caption,
                            AuthorId = p.UserId,
                            AuthorName = p.User.UserName ?? "Анонимен",
                            UploadedOn = p.UploadedOn
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<IEnumerable<HutCardViewModel>> GetTopRatedAsync(int count)
        {
            return await context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted)
                .OrderByDescending(h => h.Reviews.Any() ? h.Reviews.Average(r => r.Rating) : 0)
                .ThenByDescending(h => h.Reviews.Count)
                .Take(count)
                .Select(Projections.HutCard)
                .ToListAsync();
        }

        public async Task<IEnumerable<HutOptionViewModel>> GetWithoutKeeperAsync()
        {
            return await context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted && h.OwnerId == null)
                .OrderBy(h => h.Mountain.Name)
                .ThenBy(h => h.Name)
                .Select(h => new HutOptionViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    MountainName = h.Mountain.Name
                })
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Huts.AnyAsync(h => h.Id == id && !h.IsDeleted);
        }

        public async Task<(double Latitude, double Longitude)?> GetLocationAsync(int id)
        {
            var location = await context.Huts
                .AsNoTracking()
                .Where(h => h.Id == id && !h.IsDeleted)
                .Select(h => new { h.Latitude, h.Longitude })
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return null;
            }

            return (location.Latitude, location.Longitude);
        }

        public async Task<IEnumerable<AdminHutListItemViewModel>> GetAllForAdminAsync()
        {
            return await context.Huts
                .AsNoTracking()
                .Where(h => !h.IsDeleted)
                .OrderBy(h => h.Mountain.Name)
                .ThenBy(h => h.Name)
                .Select(h => new AdminHutListItemViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    MountainName = h.Mountain.Name,
                    Altitude = h.Altitude,
                    BedsCount = h.BedsCount,
                    OwnerEmail = h.Owner != null ? h.Owner.Email : null
                })
                .ToListAsync();
        }

        public async Task<HutFormModel?> GetForEditAsync(int id)
        {
            return await context.Huts
                .AsNoTracking()
                .Where(h => h.Id == id && !h.IsDeleted)
                .Select(h => new HutFormModel
                {
                    Name = h.Name,
                    Description = h.Description,
                    MountainId = h.MountainId,
                    Altitude = h.Altitude,
                    Latitude = h.Latitude,
                    Longitude = h.Longitude,
                    BedsCount = h.BedsCount,
                    PricePerNight = h.PricePerNight,
                    HasWater = h.HasWater,
                    HasElectricity = h.HasElectricity,
                    HasFood = h.HasFood,
                    PhoneNumber = h.PhoneNumber,
                    ImageUrl = h.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(HutFormModel model)
        {
            var hut = new Hut();
            MapFormToEntity(model, hut);

            await context.Huts.AddAsync(hut);
            await context.SaveChangesAsync();

            return hut.Id;
        }

        public async Task<bool> EditAsync(int id, HutFormModel model)
        {
            Hut? hut = await context.Huts.FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
            if (hut == null)
            {
                return false;
            }

            MapFormToEntity(model, hut);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<HutDeleteViewModel?> GetForDeleteAsync(int id)
        {
            DateTime today = DateTime.Today;

            return await context.Huts
                .AsNoTracking()
                .Where(h => h.Id == id && !h.IsDeleted)
                .Select(h => new HutDeleteViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    MountainName = h.Mountain.Name,
                    ActiveReservationsCount = h.Reservations.Count(r =>
                        r.CheckOut >= today &&
                        (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Confirmed))
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Hut? hut = await context.Huts.FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
            if (hut == null)
            {
                return false;
            }

            hut.IsDeleted = true;
            hut.OwnerId = null;

            await context.SaveChangesAsync();
            return true;
        }

        private static void MapFormToEntity(HutFormModel model, Hut hut)
        {
            hut.Name = model.Name.Trim();
            hut.Description = model.Description.Trim();
            hut.MountainId = model.MountainId;
            hut.Altitude = model.Altitude;
            hut.Latitude = model.Latitude;
            hut.Longitude = model.Longitude;
            hut.BedsCount = model.BedsCount;
            hut.PricePerNight = model.PricePerNight;
            hut.HasWater = model.HasWater;
            hut.HasElectricity = model.HasElectricity;
            hut.HasFood = model.HasFood;
            hut.PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();
            hut.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim();
        }
    }
}
