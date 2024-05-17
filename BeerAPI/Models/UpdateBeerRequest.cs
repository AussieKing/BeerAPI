namespace BeerAPI.Models
{
    public class UpdateBeerRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? PromoPrice { get; set; }
    }
}
