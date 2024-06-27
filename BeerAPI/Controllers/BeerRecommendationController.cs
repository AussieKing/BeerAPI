using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerRecommendationController : ControllerBase
    {
        // create a service to call OpenWeatherMap API and fetch the current weather
        private readonly WeatherService _weatherService;

        public BeerRecommendationController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("recommend")]
        public async Task<IActionResult> RecommendBeer(string city)
        {
            var weatherData = await _weatherService.GetCurrentWeatherAsync(city);

            // Deserialze from JSON into object
            var weather = JsonConvert.DeserializeObject<WeatherResponse>(weatherData);

            // Recommend beer based on weather
            var beer = RecommendBeerBasedOnWeather(weather);

            return Ok(new { Beer = beer });
        }

        private string RecommendBeerBasedOnWeather(WeatherResponse weather)
        {
            // harcoded recommends using weather API documentation: main.temp Temperature.
            // Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit
            if (weather.Main.Temp > 27)
            {
                return "a refreshing Lager";
            }
            else if (weather.Main.Temp > 10)
            {
                return "a smooth Pale Ale";
            }
            else
            {
                return "A robust stout";
            }
        }
    }
}
