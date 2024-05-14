using BeerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerAPI.Data;

namespace BeerAPI.Repositories
{
    public class TrolleyRepository : ITrolleyRepository
    {
        private readonly ApplicationDbContext _context;

        public TrolleyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBeerToTrolleyAsync(Beer beer)
        {
            var trolleyItem = await _context.TrolleyItems.Include(t => t.Beer).FirstOrDefaultAsync(t => t.Beer.Id == beer.Id);
            if (trolleyItem != null)
            {
                trolleyItem.Quantity++;
            }
            else
            {
                await _context.TrolleyItems.AddAsync(new TrolleyItem { Beer = beer, Quantity = 1 });
            }
            _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveBeerFromTrolleyAsync(int beerId)
        {
            var trolleyItem = await _context.TrolleyItems.Include(t => t.Beer).FirstOrDefaultAsync(t => t.Beer.Id == beerId);
            if (trolleyItem != null)
            {
                if (trolleyItem.Quantity > 1)
                {
                    trolleyItem.Quantity--;
                }
                else
                {
                    _context.TrolleyItems.Remove(trolleyItem);
                }
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Trolley> GetTrolleyAsync()
        {
            var trolleyItems = await _context.TrolleyItems.Include(t => t.Beer).ToListAsync();

            return new Trolley { Items = trolleyItems };
        }
    }
}
