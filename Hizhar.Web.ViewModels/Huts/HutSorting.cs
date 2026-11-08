using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Web.ViewModels.Huts
{
    public enum HutSorting
    {
        [Display(Name = "Най-високо оценен")]
        Rating = 0,

        [Display(Name = "Най-високо разположение")]
        Altitude = 1,

        [Display(Name = "Най-много легла")]
        Beds = 2,

        [Display(Name = "Най-ниска цена")]
        Price = 3,

        [Display(Name = "По име")]
        Name = 4
    }
}
