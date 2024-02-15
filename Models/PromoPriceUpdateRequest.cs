namespace BeerAPI.Models
{
    public class PromoPriceUpdateRequest
    {
        public decimal NewPromoPrice { get; set; }
    }
}

// This Model handles the update price promo logic. I had issues with the JSON request response, 
// so I had to create a new model to handle the request. 
// I'll change the UpdatePromoPrice method to PromoPriceUpdateRequest instance from this model, 
// instead of using the decimal type directly (which was causing the issue on Insomnia).