using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPi.Repository
{
    public interface ILocationRepository
    {
        ConcurrentDictionary<string, IEnumerable<WeatherForecast>> Locations { get; set; }

        Task<IEnumerable<WeatherForecast>> GetAsync(string name);
        Task SaveAsync(string name, IEnumerable<WeatherForecast> forecast);
    }
}