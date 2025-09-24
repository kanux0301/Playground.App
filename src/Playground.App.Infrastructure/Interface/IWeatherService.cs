namespace Playground.App.Infrastructure.Interface
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetHourlyForecastAsync(double latitude, double longitude);
    }

}
