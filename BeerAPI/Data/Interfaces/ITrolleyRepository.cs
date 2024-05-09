using BeerAPI.Models;

namespace BeerAPI.Repositories
{
    public interface ITrolleyRepository
    {
        object Items { get; }

        void AddBeerToTrolley(Beer beer);
        bool RemoveBeerFromTrolley(int beerId);
        Trolley GetTrolley();
    }
}

