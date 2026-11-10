using Hizhar.Web.ViewModels.Mountains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hizhar.Web.ViewModels.Peaks
{
    public class AllPeaksQueryModel
    {
        [Display(Name = "Търсене")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Планина")]
        public int? MountainId { get; set; }

        [Display(Name = "Мин. височина (м)")]
        public int? MinElevation { get; set; }

        [Display(Name = "Само национални обекти")]
        public bool OnlyNational { get; set; }

        public IEnumerable<PeakListItemViewModel> Peaks { get; set; } = new List<PeakListItemViewModel>();

        public IEnumerable<MountainOptionViewModel> Mountains { get; set; } = new List<MountainOptionViewModel>();
    }
}
