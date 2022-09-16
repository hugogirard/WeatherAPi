using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPi.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherByLocationAsync(string name);

        Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
    }
}