using System.ComponentModel.DataAnnotations;
using Hizhar.Web.ViewModels.Validation;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Reservation;

namespace Hizhar.Web.ViewModels.Reservations
{
    public class ReservationFormModel
    {
        public int HutId { get; set; }

        public string HutName { get; set; } = string.Empty;

        public int BedsCount { get; set; }

        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [DataType(DataType.Date)]
        [NotInPast]
        [Display(Name = "Пристигане")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [DataType(DataType.Date)]
        [DateGreaterThan(nameof(CheckIn))]
        [Display(Name = "Напускане")]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Range(GuestsMin, GuestsMax, ErrorMessage = RangeField)]
        [Display(Name = "Брой легла")]
        public int Guests { get; set; } = 1;

        [StringLength(NoteMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Бележка към хижаря")]
        public string? Note { get; set; }
    }
}
