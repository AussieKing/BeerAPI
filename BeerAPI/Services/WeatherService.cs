using System.Net.Http;
using System.Threading.Tasks;
using BeerAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BeerAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiSettings _settings;

        public WeatherService(HttpClient httpClient, IOptions<WeatherApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<string> GetCurrentWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_settings.ApiKey}&units=metric"
            );
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
