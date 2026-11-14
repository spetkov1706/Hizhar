namespace Hizhar.Web.ViewModels.Weather
{
    public class WeatherForecastViewModel
    {
        public bool IsAvailable { get; set; }

        public IEnumerable<WeatherDayViewModel> Days { get; set; } = new List<WeatherDayViewModel>();
    }
}
