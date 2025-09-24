using MediatR;
using Playground.App.Infrastructure.Interface;

namespace Playground.App.Application.Weather.Query
{
    public class GetHourWeatherQueryHandler : IRequestHandler<GetHourWeatherQuery, IEnumerable<WeatherForecast>>
    {
        private readonly IWeatherService _weatherService;

        public GetHourWeatherQueryHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IEnumerable<WeatherForecast>> Handle(GetHourWeatherQuery request, CancellationToken cancellationToken)
        {
            return await _weatherService.GetHourlyForecastAsync(request.Latitude, request.Longitude);
        }
    }
}
