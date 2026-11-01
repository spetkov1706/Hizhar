using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum ConditionType
    {
        [Display(Name = "Чиста пътека")]
        Clear = 1,

        [Display(Name = "Сняг")]
        Snow = 2,

        [Display(Name = "Заледяване")]
        Ice = 3,

        [Display(Name = "Кал")]
        Mud = 4,

        [Display(Name = "Паднали дървета")]
        FalleTress = 5,

        [Display(Name = "Мъгла")]
        Fog = 6,

        [Display(Name = "Друго")]
        Other = 7
    }
}
