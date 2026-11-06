using Hizhar.Data.Models.Enums;

namespace Hizhar.Web.ViewModels.TrailConditions
{
    public class TrailConditionViewModel
    {
        public int Id { get; set; }

        public int TrailId { get; set; }

        public string TrailName { get; set; } = null!;

        public ConditionType ConditionType { get; set; }

        public string Description { get; set; } = null!;

        public DateTime ReportedOn { get; set; }

        public string AuthorName { get; set; } = null!;

        public int DaysAgo => Math.Max(0, (int)(DateTime.UtcNow - ReportedOn).TotalDays);

        public string FreshnessText => DaysAgo switch
        {
            0 => "днес",
            1 => "вчера",
            _ => $"преди {DaysAgo} дни"
        };

        public string FreshnessCssClass => DaysAgo switch
        {
            <= 2 => "fresh",
            <= 7 => "recent",
            _ => "old"
        };
    }
}
