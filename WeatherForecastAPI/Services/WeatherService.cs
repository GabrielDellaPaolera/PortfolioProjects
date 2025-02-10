using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=646f8350e349aa52b635d74055699b0d");
                _logger.LogInformation("Response from OpenWeatherMap: {Response}", response);

                // Desserializar a resposta JSON em um objeto fortemente tipado
                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);

                if (weatherData == null)
                {
                    throw new Exception("Invalid JSON response from OpenWeatherMap");
                }

                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching weather data from OpenWeatherMap for city: {City}", city);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error parsing JSON response from OpenWeatherMap for city: {City}", city);
                throw new Exception("Invalid JSON response from OpenWeatherMap");
            }
        }
    }
}