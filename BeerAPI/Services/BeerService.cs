using BeerAPI.Models;
using BeerAPI.Services;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using BeerAPI.Data.Interfaces;

namespace BeerAPI.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository) 
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
            var beer = await GetBeerByIdAsync(id);
            if (beer == null)
            {
                throw new NotFoundException();
            }

            await _beerRepository.DeleteBeerAsync(id);
            return true;
        }

        // changed the UpdateBeerAsync to now handle exceptions better and more specifically
        public async Task UpdateBeerAsync(int id, UpdateBeerRequest updateBeerRequest)
        {
            var existingBeer = await GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                throw new NotFoundException();
            }

            existingBeer.Name = updateBeerRequest.Name;
            existingBeer.Price = updateBeerRequest.Price;
            existingBeer.PromoPrice = updateBeerRequest.PromoPrice;

            await _beerRepository.UpdateBeerAsync(existingBeer);
        }

        // introducing the UpdatePromoPriceAsyn here
        public async Task UpdatePromoPriceAsync(int id, decimal? newPromoPrice)
        {
            var existingBeer = await _beerRepository.GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                throw new NotFoundException();
            }

            existingBeer.PromoPrice = newPromoPrice;
            await _beerRepository.UpdateBeerAsync(existingBeer);
        }
    }
}
