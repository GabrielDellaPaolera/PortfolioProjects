using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeatherForecastAPI.Services;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                var weather = await _weatherService.GetWeatherAsync(city);
                if (weather == null)
                {
                    _logger.LogWarning("Weather data not found for city: {City}", city);
                    return NotFound();
                }
                return Ok(weather); // Certifique-se de que estamos retornando Ok(weather)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting weather data for city: {City}", city);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}