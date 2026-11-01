using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum RequestStatus
    {
        [Display(Name = "Чакаща")]
        Pending = 1,

        [Display(Name = "Одобрена")]
        Approved = 2,

        [Display(Name = "Отхвърлена")]
        Rejected = 3
    }
}