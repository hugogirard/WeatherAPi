using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPi.Services
{

    public class WeatherService : IWeatherService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(ILocationRepository locationRepository, IWeatherRepository weatherRepository)
        {
            _locationRepository = locationRepository;
            _weatherRepository = weatherRepository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherAsync()
        {
            return await _weatherRepository.GetAsync(string.Empty);
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherByLocationAsync(string name)
        {
            var forecast = await _locationRepository.GetAsync(name);

            if (forecast == null || forecast.Count() == 0)
            {
                forecast = await _weatherRepository.GetAsync(name);
                await _locationRepository.SaveAsync(name, forecast);
            }

            return forecast;
        }
    }
}