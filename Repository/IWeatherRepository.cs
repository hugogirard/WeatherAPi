using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPi.Repository
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<WeatherForecast>> GetAsync(string name);
    }
}