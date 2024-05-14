namespace BeerAPI.Models
{
    public class Beer 
    {
        public int Id { get; set; } 
        public string? Name { get; set; } 
        public decimal Price { get; set; } 
        public decimal? PromoPrice { get; set; } 
    }
}