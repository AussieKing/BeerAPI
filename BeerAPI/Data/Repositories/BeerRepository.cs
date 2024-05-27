using BeerAPI.Models;
using BeerAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeerAPI.Services;

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
                existingBeer.Name = beer.Name; 
                existingBeer.Price = beer.Price;
                existingBeer.PromoPrice = beer.PromoPrice;

                _context.Beers.Update(existingBeer);  
                await _context.SaveChangesAsync();             
             }
            else
            {
                throw new Exception("Beer not found!");
            }
        } 

        public async Task DeleteBeerAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)  // BETTER FOR NESTING PURPOSES 
            {
                throw new NotFoundException(); // better error throwing
            }
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }
    }
}
