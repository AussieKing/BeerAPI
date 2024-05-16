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
            var existingBeer = await _context.Beers.FindAsync(beer.Id);
            if (existingBeer != null)
            {
                _context.Entry(existingBeer).State = EntityState.Detached; // Detach the existing entity to solve the tracking of multiple instances 9of the same entity)
            }
            _context.Beers.Update(beer);
            await _context.SaveChangesAsync();
        }


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
