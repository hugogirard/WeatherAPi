using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace WeatherAPi.Repository
{

    public class DocumentLocationRepository : ILocationRepository
    {
        private readonly Container _container;

        public DocumentLocationRepository(IConfiguration configuration)
        {
            var cosmosClient = new CosmosClient(accountEndpoint: configuration["CosmosDbEndpoint"], 
                                                authKeyOrResourceToken: configuration["CosmosDbKey"]);
            _container = cosmosClient.GetContainer("weather","location");
        }

        public async Task<IEnumerable<WeatherForecast>> GetAsync(string name)
        {
            var weatherForecasts = new List<WeatherForecast>();

            var query = new QueryDefinition(
                query: "SELECT * FROM l where l = @location"
            )
            .WithParameter("@location", name);

            using FeedIterator<WeatherForecast> feed = _container.GetItemQueryIterator<WeatherForecast>(
                queryDefinition: query
            );

            while (feed.HasMoreResults)
            {
                FeedResponse<WeatherForecast> response = await feed.ReadNextAsync();
                foreach (WeatherForecast item in response)
                {
                    weatherForecasts.Add(item);
                }
            }            

            return weatherForecasts;
        }

        public async Task SaveAsync(string name, IEnumerable<WeatherForecast> forecast)
        {
            foreach (var item in forecast)
            {
                WeatherForecast weatherForecast = new WeatherForecast
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Date = item.Date,
                    Summary = item.Summary,
                    TemperatureC = item.TemperatureC
                };

                await _container.CreateItemAsync(weatherForecast, new PartitionKey(name));
            }
        }
    }
}