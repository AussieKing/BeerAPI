using BeerAPI.Models;
using System.Collections.Generic;

namespace BeerAPI.Services
{
    public interface ITrolleyService
    {
        Task AddItemAsync(Beer beer);
        Task<bool> RemoveItemAsync(int beerId);
        Task<Trolley> GetTrolleyAsync();
    }
}
