using System.ComponentModel.DataAnnotations;

namespace BeerAPI.Models // declaring the namespace
{
    public class Beer 
    {
        public int Id { get; set; } 

        [Required] 
        public string Name { get; set; } 

        [Required] 
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")] 
        public decimal Price { get; set; } 

        public decimal? PromoPrice { get; set; } 
    }
}
