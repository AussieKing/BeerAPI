using BeerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerAPI.Data.Interfaces
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
        Task<Beer> GetBeerByIdAsync(int id);
        Task AddBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
        Task DeleteBeerAsync(int id);
    }
}
