using BeerAPI.Models;

namespace BeerAPI.Repositories
{
    public interface ITrolleyRepository
    {
        void AddBeerToTrolley(Beer beer);
        bool RemoveBeerFromTrolley(int beerId);
        Trolley GetTrolley();
    }
}

