using Playground.App.Domain.Response;
using Playground.App.Infrastructure.Interface;
using System.Net.Http.Json;

namespace Playground.App.Infrastructure.Services
{
    public class OpenMeteoWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public OpenMeteoWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetHourlyForecastAsync(double latitude, double longitude)
        {
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m";

            var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

            var forecasts = new List<WeatherForecast>();

            for (int i = 0; i < response.Hourly.Time.Length; i++)
            {
                forecasts.Add(new WeatherForecast(
                    DateTime.Parse(response.Hourly.Time[i]),
                    response.Hourly.Temperature_2m[i]
                ));
            }

            return forecasts;
        }
    }

}
