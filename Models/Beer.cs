using System.ComponentModel.DataAnnotations;

namespace BeerAPI.Models // declaring the namespace
{
    public class Beer // declaring a new class called Beer
    {
        public int Id { get; set; } // the Beer class has an integer (Id), and get set are the getter and setter methods

        [Required] // the Name property is required (must provide it)
        public string Name { get; set; } // and has a string property called Name, which we will use to store the name of the beer

        [Required] // the price is also required (if we don't put it in,. we get a 400 error)
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")] // price must be greater than zero, and the max value is a double
        public decimal Price { get; set; } // and a decimal property called Price, which we will use to store the price of the beer

        public decimal? PromoPrice { get; set; } // optional nullable decimal property (its value can hold a decimal or be null) called PromoPrice, which we will use to store the promotional price of the beer
    }
}