public class WeatherForecast
{
    public DateTime Time { get; }
    public double Temperature { get; }

    public WeatherForecast(DateTime time, double temperature)
    {
        Time = time;
        Temperature = temperature;
    }
}
