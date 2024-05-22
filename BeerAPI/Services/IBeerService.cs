using BeerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerAPI.Services
{
    public interface IBeerService
    {
        Task<Beer> GetBeerByIdAsync(int beerId);
        Task<List<Beer>> GetAllBeersAsync();
        Task<Beer> AddBeerAsync(Beer newBeer);
        Task<bool> DeleteBeerAsync(int id);
        Task UpdateBeerAsync(int id, UpdateBeerRequest updatedBeer);
        Task UpdatePromoPriceAsync(int id, decimal? newPromoPrice);
    }
}
