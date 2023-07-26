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

    [HttpGet("{name}")]
    public async Task<IEnumerable<WeatherForecast>> Get(string name)
    {
        return await _weatherService.GetWeatherByLocationAsync(name);        
    }

    // [HttpPost]
    // [Route("saveLocation")]
    // [ProducesResponseType(typeof(IEnumerable<WeatherForecast>),200)]
    // [ProducesResponseType(400)]
    // public async Task<IActionResult> Post([FromBody] LocationViewModel vm)
    // {

    // }

    // [HttpPost]
    // [Route("dummyPost")]
    // [ProducesResponseType(typeof(IEnumerable<WeatherForecast>),200)]
    // [ProducesResponseType(400)]
    // public async Task<IActionResult> Post([FromBody] string xml)
    // {
    //     return new OkObjectResult(xml);
    // }    
}   
