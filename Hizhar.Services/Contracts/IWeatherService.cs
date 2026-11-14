using Hizhar.Web.ViewModels.Weather;

namespace Hizhar.Services.Contracts
{
    public interface IWeatherService
    {
        Task<WeatherForecastViewModel> GetForecastAsync(double latitude, double longitude);
    }
}
