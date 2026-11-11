using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Reservations;

namespace Hizhar.Services.Contracts
{
    public interface IReservationService
    {
        Task<int> GetFreeBedsAsync(int hutId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null);

        Task<ReservationFormModel?> GetFormAsync(int hutId);

        Task<OperationResult> CreateAsync(ReservationFormModel model, string userId);

        Task<IEnumerable<ReservationListItemViewModel>> GetMineAsync(string userId);

        Task<OperationResult> CancelAsync(int reservationId, string userId);


        Task<OperationResult> ConfirmAsync(int reservationId, string keeperId);

        Task<OperationResult> RejectAsync(int reservationId, string keeperId);

        Task<int> GetPendingCountForKeeperAsync(string keeperId);
    }
}
