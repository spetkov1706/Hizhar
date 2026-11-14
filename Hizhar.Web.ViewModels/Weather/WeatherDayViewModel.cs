namespace Hizhar.Web.ViewModels.Weather
{
    public class WeatherDayViewModel
    {
        public DateTime Date { get; set; }

        public double MaxTemperature { get; set; }

        public double MinTemperature { get; set; }

        public double Precipitation { get; set; }

        public string Description { get; set; } = null!;

        public string Icon { get; set; } = null!;
    }
}
