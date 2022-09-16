using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPi.Repository
{

    public class LocationRepository : ILocationRepository
    {
        public ConcurrentDictionary<string, IEnumerable<WeatherForecast>> Locations { get; set; }

        public LocationRepository()
        {
            Locations = new ConcurrentDictionary<string, IEnumerable<WeatherForecast>>();
        }

        public async Task<IEnumerable<WeatherForecast>> GetAsync(string name)
        {
            if (Locations.TryGetValue(name, out IEnumerable<WeatherForecast> value))
            {
                return await Task.FromResult(value);
            }

            return await Task.FromResult<IEnumerable<WeatherForecast>>(null);
        }

        public Task SaveAsync(string name, IEnumerable<WeatherForecast> forecast)
        {
            if (!Locations.ContainsKey(name))
            {
                Locations.TryAdd(name, forecast);
            }

            return Task.CompletedTask;
        }
    }
}