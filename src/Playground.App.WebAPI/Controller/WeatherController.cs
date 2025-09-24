using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.App.Application.Weather.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.App.WebAPI.Controller;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for handling weather forecast related requests.
/// </summary>
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = new[]
         {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering"
        };

    private readonly ILogger<WeatherController> _logger;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for the controller.</param>
    /// <param name="mediator"></param>
    public WeatherController(ILogger<WeatherController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Gets the weather forecast for the next 5 days.
    /// </summary>
    /// <returns>A list of weather forecasts</returns>
    [HttpGet("houweather")]
    [SwaggerOperation(Summary = "Get the weather forecast", Description = "Retrieves the weather forecast for the next 5 days.")]
    [SwaggerResponse(200, "Weather forecast successfully retrieved.", typeof(IEnumerable<WeatherForecast>))]
    [SwaggerResponse(500, "Internal server error.")]
    public IEnumerable<WeatherForecast> GetHourWeather()
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

    /// <summary>
    /// Gets the weather forecast for a specific latitude and longitude.
    /// </summary>
    /// <param name="lat">The latitude coordinate.</param>
    /// <param name="lon">The longitude coordinate.</param>
    /// <returns>An <see cref="IActionResult"/> containing the weather forecast data.</returns>
    [HttpGet("forecast")]
    public async Task<IActionResult> GetForecast([FromQuery] double lat, [FromQuery] double lon)
    {
        var query = new GetHourWeatherQuery(lat, lon);
        var forecast = await _mediator.Send(query);
        return Ok(forecast);
    }
}
/// <summary>
/// Represents a weather forecast for a specific date.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Gets or sets the date of the forecast.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets or sets a summary of the weather conditions.
    /// </summary>
    public required string Summary { get; set; }

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}