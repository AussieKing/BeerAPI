using BeerAPI.Models;
using BeerAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerAPI.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApplicationDbContext _context;

        public BeerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            return await _context.Beers.ToListAsync();
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        public async Task AddBeerAsync(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            var existingBeer = await _context.Beers.FindAsync(beer.Id);  // FIND
            if (existingBeer != null)  // EXISTS
            {
                existingBeer.Name = beer.Name; // CHANGE before UPDATE
                existingBeer.Price = beer.Price;
                existingBeer.PromoPrice = beer.PromoPrice;

                _context.Beers.Update(existingBeer);  // UPDATE
                await _context.SaveChangesAsync();          // SAVE     
             }
            else
            {
                throw new Exception("Beer not found!");
            }
        } 
        // solution was not to Detach, rather to only focus on the beer from the database. previously I was taking the beer object and ALSO trying to create a whole new beere and store in the same location (hence error 500)
        //TRACKE WHERE THE ID IN THE PUT REQUEST COMES FROM (DOESN'T NEED TO BE A PART OF THE JSON)
        // LOOK UP THROWING EXCEPTIONS , HERE FOR EXAMPLE THE EXCEPTION SHOULD HAVE BEEN A 404 (I COULD IGNORE , BUT THE END RESULT SHOULD BE 404)

        public async Task DeleteBeerAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
