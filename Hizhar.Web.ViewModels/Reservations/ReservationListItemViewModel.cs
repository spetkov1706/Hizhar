using Hizhar.Data.Models.Enums;

namespace Hizhar.Web.ViewModels.Reservations
{
    public class ReservationListItemViewModel
    {
        public int Id { get; set; }

        public int HutId { get; set; }

        public string HutName { get; set; } = null!;

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Guests { get; set; }

        public string? Note { get; set; }

        public ReservationStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public string GuestEmail { get; set; } = string.Empty;

        public decimal PricePerNight { get; set; }

        public int Nights => (CheckOut.Date - CheckIn.Date).Days;

        public decimal TotalPrice => Nights * Guests * PricePerNight;

        public bool CanCancel => (Status == ReservationStatus.Pending || Status == ReservationStatus.Confirmed)
                                 && CheckIn.Date > DateTime.Today;

        public string StatusCssClass => Status switch
        {
            ReservationStatus.Pending => "bg-warning text-dark",
            ReservationStatus.Confirmed => "bg-success",
            ReservationStatus.Rejected => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
