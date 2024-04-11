using BeerAPI.Models;

namespace BeerAPI.Services
{
    public interface IBeerService
    {
        Beer? GetBeerById(int beerId);
        List<Beer> GetAllBeers();
        Beer AddBeer(Beer newBeer);
        bool DeleteBeer(int id);
    }
}
