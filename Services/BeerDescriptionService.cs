//? Step 2: Create a new file called BeerDescriptionService.cs in the Services folder
// In this file, we define the BeerDescriptionService class, which implements the IBeerDescriptionService interface.

using BeerAPI.Models;

namespace BeerAPI.Services
{
    public class BeerDescriptionService : IBeerDescriptionService
    {
        public string GetDescription(Beer beer)
        {
            return $"{beer.Name} is priced at {beer.Price}. Promo Price: {beer.PromoPrice?.ToString() ?? "N/A"}";
        }
    }
}