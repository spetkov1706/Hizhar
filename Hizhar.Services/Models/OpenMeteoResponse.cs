using System.Text.Json.Serialization;

namespace Hizhar.Services.Models
{
    public class OpenMeteoResponse
    {
        [JsonPropertyName("daily")]
        public OpenMeteoDaily? Daily { get; set; }
    }
    public class OpenMeteoDaily
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = new();

        [JsonPropertyName("weather_code")]
        public List<int?> WeatherCode { get; set; } = new();

        [JsonPropertyName("temperature_2m_max")]
        public List<double?> TemperatureMax { get; set; } = new();

        [JsonPropertyName("temperature_2m_min")]
        public List<double?> TemperatureMin { get; set; } = new();

        [JsonPropertyName("precipitation_sum")]
        public List<double?> PrecipitationSum { get; set; } = new();
    }
}
