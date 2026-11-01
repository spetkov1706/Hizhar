using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum Difficulty
    {
        [Display(Name = "Лесен")]
        Easy = 1,

        [Display(Name = "Среден")]
        Medium = 2,

        [Display(Name = "Труден")]
        Hard = 3
    }
}
