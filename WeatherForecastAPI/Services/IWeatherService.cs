using System.Threading.Tasks;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string city);
    }
}