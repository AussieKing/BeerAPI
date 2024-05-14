using BeerAPI.Models;

namespace BeerAPI.Repositories
{
    public interface ITrolleyRepository
    {
        Task AddBeerToTrolleyAsync(Beer beer);
        Task<bool> RemoveBeerFromTrolleyAsync(int beerId);
        Task<Trolley> GetTrolleyAsync();
    }
}

