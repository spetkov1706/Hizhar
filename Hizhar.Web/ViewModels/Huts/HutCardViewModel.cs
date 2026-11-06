namespace Hizhar.Web.ViewModels.Huts
{
    public class HutCardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string MountainName { get; set; } = null!;

        public int Altitude { get; set; }

        public int BedsCount { get; set; }

        public decimal PricePerNight { get; set; }

        public string? ImageUrl { get; set; }

        public bool HasWater { get; set; }

        public bool HasElectricity { get; set; }

        public bool HasFood { get; set; }

        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }
    }
}
