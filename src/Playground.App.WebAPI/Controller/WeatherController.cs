using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.App.WebAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = new[]
         {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering"
        };

    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the weather forecast for the next 5 days.
    /// </summary>
    /// <returns>A list of weather forecasts</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Get the weather forecast", Description = "Retrieves the weather forecast for the next 5 days.")]
    [SwaggerResponse(200, "Weather forecast successfully retrieved.", typeof(IEnumerable<WeatherForecast>))]
    [SwaggerResponse(500, "Internal server error.")]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();

        return forecast;
    }
}
public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}