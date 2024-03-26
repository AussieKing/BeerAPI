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