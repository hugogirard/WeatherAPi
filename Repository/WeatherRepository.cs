using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPi.Repository
{

    public class WeatherRepository : IWeatherRepository
    {

        private readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<IEnumerable<WeatherForecast>> GetAsync(string name)
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                Guid.NewGuid().ToString(),
                DateTime.Now.AddDays(index),
                rng.Next(-20, 55),
                Summaries[rng.Next(Summaries.Length)],
                name
                
            )).ToArray();            

            return await Task.FromResult(result);
        }
    }
}