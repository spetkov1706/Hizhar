using Hizhar.Data.Models.Enums;

namespace Hizhar.Web.ViewModels.Trails
{
    public class TrailCardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string MountainName { get; set; } = null!;

        public double LengthKm { get; set; }

        public int ElevationGain { get; set; }

        public int DurationMinutes { get; set; }

        public Difficulty Difficulty { get; set; }

        public Season Season { get; set; }

        public string StartPoint { get; set; } = string.Empty;

        public string EndPoint { get; set; } = string.Empty;

        public ConditionType? LatestConditionType { get; set; }

        public DateTime? LatestConditionDate { get; set; }

        public int ReportsCount { get; set; }

        public string DurationText => DurationMinutes >= 60
            ? $"{DurationMinutes / 60} ч {DurationMinutes % 60:00} мин"
            : $"{DurationMinutes} мин";
    }
}
