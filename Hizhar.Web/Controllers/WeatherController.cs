using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;
        private readonly IHutService hutService;

        public WeatherController(IWeatherService weatherService, IHutService hutService)
        {
            this.weatherService = weatherService;
            this.hutService = hutService;
        }

        [HttpGet]
        public async Task<IActionResult> Hut(int id)
        {
            (double Latitude, double Longitude)? location = await hutService.GetLocationAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            WeatherForecastViewModel forecast = await weatherService.GetForecastAsync(
                location.Value.Latitude,
                location.Value.Longitude);

            return PartialView("_WeatherPartial", forecast);
        }
    }
}
