using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Reservations;
using Microsoft.EntityFrameworkCore;
using static Hizhar.Common.ValidationConstants.Reservation;

namespace Hizhar.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HizharDbContext context;

        public ReservationService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<int> GetFreeBedsAsync(int hutId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            DateTime from = checkIn.Date;
            DateTime to = checkOut.Date;

            int? bedsCount = await context.Huts
                .Where(h => h.Id == hutId && !h.IsDeleted)
                .Select(h => (int?)h.BedsCount)
                .FirstOrDefaultAsync();

            if (bedsCount == null || to <= from || (to - from).TotalDays > 366)
            {
                return 0;
            }

            var overlapping = await context.Reservations
                .AsNoTracking()
                .Where(r => r.HutId == hutId
                            && (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Confirmed)
                            && r.CheckIn < to
                            && r.CheckOut > from
                            && (excludeReservationId == null || r.Id != excludeReservationId))
                .Select(r => new { r.CheckIn, r.CheckOut, r.Guests })
                .ToListAsync();

            int minFree = bedsCount.Value;

            for (DateTime night = from; night < to; night = night.AddDays(1))
            {
                int occupied = overlapping
                    .Where(r => r.CheckIn.Date <= night && r.CheckOut.Date > night)
                    .Sum(r => r.Guests);

                minFree = Math.Min(minFree, bedsCount.Value - occupied);
            }

            return Math.Max(0, minFree);
        }

        public async Task<ReservationFormModel?> GetFormAsync(int hutId)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);

            return await context.Huts
                .AsNoTracking()
                .Where(h => h.Id == hutId && !h.IsDeleted)
                .Select(h => new ReservationFormModel
                {
                    HutId = h.Id,
                    HutName = h.Name,
                    BedsCount = h.BedsCount,
                    PricePerNight = h.PricePerNight,
                    CheckIn = tomorrow,
                    CheckOut = tomorrow.AddDays(1),
                    Guests = 1
                })
                .FirstOrDefaultAsync();
        }

        public async Task<OperationResult> CreateAsync(ReservationFormModel model, string userId)
        {
            DateTime checkIn = model.CheckIn.Date;
            DateTime checkOut = model.CheckOut.Date;

            if (checkIn < DateTime.Today)
            {
                return OperationResult.Failure("Не може да резервирате за минала дата.");
            }

            if (checkOut <= checkIn)
            {
                return OperationResult.Failure("Датата на напускане трябва да е след датата на пристигане.");
            }

            if ((checkOut - checkIn).Days > MaxNights)
            {
                return OperationResult.Failure($"Може да резервирате най-много {MaxNights} нощувки наведнъж.");
            }

            bool hutExists = await context.Huts.AnyAsync(h => h.Id == model.HutId && !h.IsDeleted);
            if (!hutExists)
            {
                return OperationResult.Failure("Хижата не съществува.");
            }

            int freeBeds = await GetFreeBedsAsync(model.HutId, checkIn, checkOut);
            if (model.Guests > freeBeds)
            {
                return OperationResult.Failure(freeBeds == 0
                    ? "За избраните дати няма свободни легла."
                    : $"За избраните дати има само {freeBeds} свободни легла.");
            }

            var reservation = new Reservation
            {
                HutId = model.HutId,
                UserId = userId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Guests = model.Guests,
                Note = string.IsNullOrWhiteSpace(model.Note) ? null : model.Note.Trim(),
                Status = ReservationStatus.Pending,
                CreatedOn = DateTime.UtcNow
            };

            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<IEnumerable<ReservationListItemViewModel>> GetMineAsync(string userId)
        {
            return await context.Reservations
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CheckIn)
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
                    PricePerNight = r.Hut.PricePerNight
                })
                .ToListAsync();
        }

        public async Task<OperationResult> CancelAsync(int reservationId, string userId)
        {
            Reservation? reservation = await context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);

            if (reservation == null)
            {
                return OperationResult.Failure("Резервацията не е намерена.");
            }

            if (reservation.Status != ReservationStatus.Pending && reservation.Status != ReservationStatus.Confirmed)
            {
                return OperationResult.Failure("Тази резервация вече не е активна.");
            }

            if (reservation.CheckIn.Date <= DateTime.Today)
            {
                return OperationResult.Failure("Не може да анулирате резервация, която вече е започнала.");
            }

            reservation.Status = ReservationStatus.Cancelled;
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<OperationResult> ConfirmAsync(int reservationId, string keeperId)
        {
            Reservation? reservation = await GetKeeperReservationAsync(reservationId, keeperId);
            if (reservation == null)
            {
                return OperationResult.Failure("Резервацията не е намерена.");
            }

            if (reservation.Status != ReservationStatus.Pending)
            {
                return OperationResult.Failure("Може да потвърждавате само чакащи резервации.");
            }

            int freeBeds = await GetFreeBedsAsync(reservation.HutId, reservation.CheckIn, reservation.CheckOut, reservation.Id);
            if (reservation.Guests > freeBeds)
            {
                return OperationResult.Failure("Няма достатъчно свободни легла, за да потвърдите тази резервация.");
            }

            reservation.Status = ReservationStatus.Confirmed;
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<OperationResult> RejectAsync(int reservationId, string keeperId)
        {
            Reservation? reservation = await GetKeeperReservationAsync(reservationId, keeperId);
            if (reservation == null)
            {
                return OperationResult.Failure("Резервацията не е намерена.");
            }

            if (reservation.Status != ReservationStatus.Pending)
            {
                return OperationResult.Failure("Може да отказвате само чакащи резервации.");
            }

            reservation.Status = ReservationStatus.Rejected;
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<int> GetPendingCountForKeeperAsync(string keeperId)
        {
            return await context.Reservations
                .CountAsync(r => r.Hut.OwnerId == keeperId
                                 && !r.Hut.IsDeleted
                                 && r.Status == ReservationStatus.Pending);
        }

        private async Task<Reservation?> GetKeeperReservationAsync(int reservationId, string keeperId)
        {
            return await context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId && r.Hut.OwnerId == keeperId);
        }
    }
}
