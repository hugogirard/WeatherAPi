using Microsoft.AspNetCore.Mvc;

namespace WeatherAPi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await _weatherService.GetWeatherAsync();
    }

    [HttpPost]
    [Route("saveLocation")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>),200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] LocationViewModel vm)
    {
        if (string.IsNullOrEmpty(vm.Name))
        {
            return new BadRequestObjectResult("The name of the location cannot be null");
        }

        var result = await _weatherService.GetWeatherByLocationAsync(vm.Name);

        return new OkObjectResult(result);
    }
}   
