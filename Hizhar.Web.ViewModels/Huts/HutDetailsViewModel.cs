using Hizhar.Web.ViewModels.Photos;
using Hizhar.Web.ViewModels.Reviews;

namespace Hizhar.Web.ViewModels.Huts
{
    public class HutDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MountainId { get; set; }

        public string MountainName { get; set; } = null!;

        public int Altitude { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int BedsCount { get; set; }

        public decimal PricePerNight { get; set; }

        public bool HasWater { get; set; }

        public bool HasElectricity { get; set; }

        public bool HasFood { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public string? KeeperEmail { get; set; }

        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public int FreeBedsTonight { get; set; }

        public bool IsFavorite { get; set; }

        public bool HasReviewed { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();

        public IEnumerable<PhotoViewModel> Photos { get; set; } = new List<PhotoViewModel>();
    }
}
