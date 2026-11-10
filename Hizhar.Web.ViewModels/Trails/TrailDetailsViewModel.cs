using Hizhar.Data.Models.Enums;
using Hizhar.Web.ViewModels.TrailConditions;

namespace Hizhar.Web.ViewModels.Trails
{
    public class TrailDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MountainId { get; set; }

        public string MountainName { get; set; } = null!;

        public double LengthKm { get; set; }

        public int ElevationGain { get; set; }

        public int DurationMinutes { get; set; }

        public Difficulty Difficulty { get; set; }

        public Season Season { get; set; }

        public IEnumerable<TrailPointViewModel> Points { get; set; } = new List<TrailPointViewModel>();

        public IEnumerable<TrailConditionViewModel> Conditions { get; set; } = new List<TrailConditionViewModel>();

        /// <summary>Празна форма за нов доклад – показва се под списъка с доклади.</summary>
        public TrailConditionFormModel NewCondition { get; set; } = new TrailConditionFormModel();

        public string DurationText => DurationMinutes >= 60
            ? $"{DurationMinutes / 60} ч {DurationMinutes % 60:00} мин"
            : $"{DurationMinutes} мин";
    }
}
