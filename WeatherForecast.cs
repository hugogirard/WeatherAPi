using System.Text.Json.Serialization;

namespace WeatherAPi;

public record WeatherForecast(string id, 
                              DateTime date, 
                              int temperatureC,                               
                              string? summary, 
                              string name);
