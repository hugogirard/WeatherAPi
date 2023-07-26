using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IWeatherRepository,WeatherRepository>();

// Validate if we use in memory repository
bool useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryRepository", true);

if (useInMemory) 
{
    builder.Services.AddSingleton<ILocationRepository, LocationRepositoryInMemory>();
}
else 
{

}

builder.Services.AddSingleton<IWeatherService, WeatherService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherApi", Version = "v1", Description = "Weather API" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherApi v1");
    c.RoutePrefix = string.Empty;   
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
