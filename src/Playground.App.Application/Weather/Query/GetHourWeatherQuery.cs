using MediatR;

namespace Playground.App.Application.Weather.Query 
{
    public class GetHourWeatherQuery : IRequest<IEnumerable<WeatherForecast>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GetHourWeatherQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
