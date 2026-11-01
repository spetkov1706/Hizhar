using System;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Data.Models.Enums
{
    public enum TrailPointType
    {
        [Display(Name = "Изходен пункт")]
        Trailhead = 1,

        [Display(Name = "Хижа")]
        Hut = 2,

        [Display(Name = "Връх")]
        Peak = 3,

        [Display(Name = "Междинна точка")]
        Waypoint = 4
    }
}
