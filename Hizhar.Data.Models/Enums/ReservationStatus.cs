using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum ReservationStatus
    {
        [Display(Name = "Чакаща")]
        Pending = 1,

        [Display(Name = "Потвърдена")]
        Confirmed = 2,

        [Display(Name = "Отказана")]
        Rejected = 3,

        [Display(Name = "Анулирана")]
        Cancelled = 4
    }
}
