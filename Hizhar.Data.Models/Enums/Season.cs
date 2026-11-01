using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum Season
    {
        [Display(Name = "Целогодишно")]
        AllYear = 1,

        [Display(Name = "Само лято")]
        Summer = 2,

        [Display(Name = "Пролет - есен")]
        SpringToAutumn = 3,

        [Display(Name = "Зимен маршрут")]
        Winter = 4
    }
}
