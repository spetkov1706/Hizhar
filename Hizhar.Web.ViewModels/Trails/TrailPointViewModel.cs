using Hizhar.Data.Models.Enums;

namespace Hizhar.Web.ViewModels.Trails
{
    public class TrailPointViewModel
    {
        public int Order { get; set; }

        public string Name { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TrailPointType PointType { get; set; }
    }
}
