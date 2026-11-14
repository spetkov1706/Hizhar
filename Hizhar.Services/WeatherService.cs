using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Weather;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Http.Json;

namespace Hizhar.Services
{
    public class WeatherService : IWeatherService
    {
        private const int ForecastDays = 3;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        private readonly HttpClient httpClient;
        private readonly IMemoryCache cache;
        private readonly ILogger<WeatherService> logger;

        public WeatherService(HttpClient httpClient, IMemoryCache cache, ILogger<WeatherService> logger)
        {
            this.httpClient = httpClient;
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<WeatherForecastViewModel> GetForecastAsync(double latitude, double longitude)
        {
            string lat = latitude.ToString("0.####", CultureInfo.InvariantCulture);
            string lon = longitude.ToString("0.####", CultureInfo.InvariantCulture);
            string cacheKey = $"weather_{lat}_{lon}";

            if (cache.TryGetValue(cacheKey, out WeatherForecastViewModel? cached) && cached != null)
            {
                return cached;
            }

            string url = $"v1/forecast?latitude={lat}&longitude={lon}" +
                         "&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum" +
                         $"&timezone=auto&forecast_days={ForecastDays}";

            try
            {
                OpenMeteoResponse? response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

                if (response?.Daily == null || response.Daily.Time.Count == 0)
                {
                    return new WeatherForecastViewModel { IsAvailable = false };
                }

                OpenMeteoDaily daily = response.Daily;
                var days = new List<WeatherDayViewModel>();

                for (int i = 0; i < daily.Time.Count; i++)
                {
                    int code = daily.WeatherCode.ElementAtOrDefault(i) ?? 0;
                    (string description, string icon) = DescribeWeatherCode(code);

                    days.Add(new WeatherDayViewModel
                    {
                        Date = DateTime.Parse(daily.Time[i], CultureInfo.InvariantCulture),
                        MaxTemperature = daily.TemperatureMax.ElementAtOrDefault(i) ?? 0,
                        MinTemperature = daily.TemperatureMin.ElementAtOrDefault(i) ?? 0,
                        Precipitation = daily.PrecipitationSum.ElementAtOrDefault(i) ?? 0,
                        Description = description,
                        Icon = icon
                    });
                }

                var forecast = new WeatherForecastViewModel { IsAvailable = true, Days = days };
                cache.Set(cacheKey, forecast, CacheDuration);

                return forecast;
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException || ex is System.Text.Json.JsonException)
            {
                logger.LogWarning(ex, "Неуспешно извличане на прогноза за {Lat}, {Lon}", lat, lon);
                return new WeatherForecastViewModel { IsAvailable = false };
            }
        }

        private static (string Description, string Icon) DescribeWeatherCode(int code) => code switch
        {
            0 => ("Ясно", "sun"),
            1 or 2 => ("Предимно слънчево", "cloud-sun"),
            3 => ("Облачно", "clouds"),
            45 or 48 => ("Мъгла", "cloud-fog2"),
            >= 51 and <= 57 => ("Ръмеж", "cloud-drizzle"),
            >= 61 and <= 67 => ("Дъжд", "cloud-rain"),
            >= 71 and <= 77 => ("Сняг", "cloud-snow"),
            >= 80 and <= 82 => ("Превалявания", "cloud-rain-heavy"),
            85 or 86 => ("Снеговалеж", "snow"),
            >= 95 => ("Гръмотевици", "cloud-lightning-rain"),
            _ => ("Променливо", "cloud")
        };
    }
}
