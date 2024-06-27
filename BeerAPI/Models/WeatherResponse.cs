namespace BeerAPI.Models
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
    }
}
