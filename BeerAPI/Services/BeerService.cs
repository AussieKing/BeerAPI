using BeerAPI.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using BeerAPI.Data.Interfaces;

namespace BeerAPI.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository; // changed to IBeerRepository instead of IDbConnection

        public BeerService(IBeerRepository beerRepository) // constructor now accepting IBeerRepository instead of connecton string
        {
            _beerRepository = beerRepository;
        }

        // Updating all methods to use the repository
        public async Task<Beer> GetBeerByIdAsync(int beerId)
        {
            return await _beerRepository.GetBeerByIdAsync(beerId);
        }

        public async Task<List<Beer>> GetAllBeersAsync()
        {
            var beers = await _beerRepository.GetAllBeersAsync();
            return new List<Beer>(beers);
        }

        public async Task<Beer> AddBeerAsync(Beer newBeer)
        {
            await _beerRepository.AddBeerAsync(newBeer);
            return newBeer;
        }

        public async Task<bool> DeleteBeerAsync(int id)
        {
            await _beerRepository.DeleteBeerAsync(id);
            return true;
        }

        public async Task UpdateBeerAsync(Beer updatedBeer)
        {
            await _beerRepository.UpdateBeerAsync(updatedBeer);
        }
    }
}
