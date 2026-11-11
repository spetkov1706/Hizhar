using Hizhar.Common;
using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.HutKeeper;
using Hizhar.Web.ViewModels.Reservations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class HutKeeperService : IHutKeeperService
    {
        private readonly HizharDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationService reservationService;

        public HutKeeperService(
            HizharDbContext context,
            UserManager<ApplicationUser> userManager,
            IReservationService reservationService)
        {
            this.context = context;
            this.userManager = userManager;
            this.reservationService = reservationService;
        }

        public async Task<bool> HasPendingRequestAsync(string userId)
        {
            return await context.HutKeeperRequests
                .AnyAsync(r => r.UserId == userId && r.Status == RequestStatus.Pending);
        }

        public async Task<OperationResult> CreateRequestAsync(HutKeeperRequestFormModel model, string userId)
        {
            if (await HasPendingRequestAsync(userId))
            {
                return OperationResult.Failure("Вече имате подадена заявка, която чака одобрение.");
            }

            bool alreadyKeeper = await context.Huts.AnyAsync(h => h.OwnerId == userId && !h.IsDeleted);
            if (alreadyKeeper)
            {
                return OperationResult.Failure("Вие вече управлявате хижа.");
            }

            Hut? hut = await context.Huts.FirstOrDefaultAsync(h => h.Id == model.HutId && !h.IsDeleted);
            if (hut == null)
            {
                return OperationResult.Failure("Хижата не съществува.");
            }

            if (hut.OwnerId != null)
            {
                return OperationResult.Failure("Тази хижа вече има хижар.");
            }

            await context.HutKeeperRequests.AddAsync(new HutKeeperRequest
            {
                HutId = model.HutId,
                UserId = userId,
                Message = model.Message.Trim(),
                Status = RequestStatus.Pending,
                CreatedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<IEnumerable<HutKeeperRequestViewModel>> GetRequestsAsync()
        {
            return await context.HutKeeperRequests
                .AsNoTracking()
                .OrderBy(r => r.Status)              // първо чакащите (Pending = 1)
                .ThenByDescending(r => r.CreatedOn)
                .Select(r => new HutKeeperRequestViewModel
                {
                    Id = r.Id,
                    UserEmail = r.User.Email ?? string.Empty,
                    HutId = r.HutId,
                    HutName = r.Hut.Name,
                    Message = r.Message,
                    Status = r.Status,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<int> GetPendingRequestsCountAsync()
        {
            return await context.HutKeeperRequests.CountAsync(r => r.Status == RequestStatus.Pending);
        }

        public async Task<OperationResult> ApproveRequestAsync(int requestId)
        {
            HutKeeperRequest? request = await context.HutKeeperRequests
                .Include(r => r.Hut)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null || request.Status != RequestStatus.Pending)
            {
                return OperationResult.Failure("Заявката не е намерена или вече е обработена.");
            }

            if (request.Hut.IsDeleted || request.Hut.OwnerId != null)
            {
                return OperationResult.Failure("Хижата вече има хижар или е изтрита.");
            }

            request.Status = RequestStatus.Approved;
            request.Hut.OwnerId = request.UserId;

            // Останалите чакащи заявки за същата хижа се отхвърлят автоматично
            List<HutKeeperRequest> otherRequests = await context.HutKeeperRequests
                .Where(r => r.HutId == request.HutId && r.Id != request.Id && r.Status == RequestStatus.Pending)
                .ToListAsync();

            foreach (HutKeeperRequest other in otherRequests)
            {
                other.Status = RequestStatus.Rejected;
            }

            await context.SaveChangesAsync();

            if (!await userManager.IsInRoleAsync(request.User, RoleConstants.HutKeeper))
            {
                await userManager.AddToRoleAsync(request.User, RoleConstants.HutKeeper);
            }

            return OperationResult.Success();
        }

        public async Task<OperationResult> RejectRequestAsync(int requestId)
        {
            HutKeeperRequest? request = await context.HutKeeperRequests.FindAsync(requestId);

            if (request == null || request.Status != RequestStatus.Pending)
            {
                return OperationResult.Failure("Заявката не е намерена или вече е обработена.");
            }

            request.Status = RequestStatus.Rejected;
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<HutKeeperDashboardViewModel?> GetDashboardAsync(string keeperId)
        {
            var hut = await context.Huts
                .AsNoTracking()
                .Where(h => h.OwnerId == keeperId && !h.IsDeleted)
                .Select(h => new { h.Id, h.Name, h.BedsCount })
                .FirstOrDefaultAsync();

            if (hut == null)
            {
                return null;
            }

            DateTime today = DateTime.Today;

            List<ReservationListItemViewModel> reservations = await context.Reservations
                .AsNoTracking()
                .Where(r => r.HutId == hut.Id
                            && r.CheckOut >= today
                            && (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Confirmed))
                .OrderBy(r => r.CheckIn)
                .Select(r => new ReservationListItemViewModel
                {
                    Id = r.Id,
                    HutId = r.HutId,
                    HutName = r.Hut.Name,
                    CheckIn = r.CheckIn,
                    CheckOut = r.CheckOut,
                    Guests = r.Guests,
                    Note = r.Note,
                    Status = r.Status,
                    CreatedOn = r.CreatedOn,
                    GuestEmail = r.User.Email ?? string.Empty,
                    PricePerNight = r.Hut.PricePerNight
                })
                .ToListAsync();

            return new HutKeeperDashboardViewModel
            {
                HutId = hut.Id,
                HutName = hut.Name,
                BedsCount = hut.BedsCount,
                FreeBedsTonight = await reservationService.GetFreeBedsAsync(hut.Id, today, today.AddDays(1)),
                PendingReservations = reservations.Where(r => r.Status == ReservationStatus.Pending).ToList(),
                UpcomingReservations = reservations.Where(r => r.Status == ReservationStatus.Confirmed).ToList()
            };
        }

        public async Task<HutKeeperHutFormModel?> GetHutForEditAsync(string keeperId)
        {
            return await context.Huts
                .AsNoTracking()
                .Where(h => h.OwnerId == keeperId && !h.IsDeleted)
                .Select(h => new HutKeeperHutFormModel
                {
                    Name = h.Name,
                    Description = h.Description,
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

        public async Task<bool> EditHutAsync(string keeperId, HutKeeperHutFormModel model)
        {
            Hut? hut = await context.Huts.FirstOrDefaultAsync(h => h.OwnerId == keeperId && !h.IsDeleted);
            if (hut == null)
            {
                return false;
            }

            hut.Description = model.Description.Trim();
            hut.BedsCount = model.BedsCount;
            hut.PricePerNight = model.PricePerNight;
            hut.HasWater = model.HasWater;
            hut.HasElectricity = model.HasElectricity;
            hut.HasFood = model.HasFood;
            hut.PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();
            hut.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim();

            await context.SaveChangesAsync();
            return true;
        }
    }
}
