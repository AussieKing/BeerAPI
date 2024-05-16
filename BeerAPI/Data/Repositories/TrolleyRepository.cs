using BeerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerAPI.Data;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var trolleyItem = await _context.TrolleyItems.FirstOrDefaultAsync(ti => ti.Beer.Id == beer.Id);
            if (trolleyItem != null)
            {
                trolleyItem.Quantity +=1;
                _context.TrolleyItems.Update(trolleyItem);
            }
            else
            {
                _context.TrolleyItems.Add(new TrolleyItem { BeerId = beer.Id, Quantity = 1});
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveBeerFromTrolleyAsync(int beerId)
        {
            var trolleyItem = await _context.TrolleyItems.FirstOrDefaultAsync(ti => ti.Beer.Id == beerId);
            if (trolleyItem != null)
            {
                if (trolleyItem.Quantity > 1)
                {
                    trolleyItem.Quantity -= 1; // removing by one, fixing the previous removing all
                    _context.TrolleyItems.Update(trolleyItem);

                }
                else
                {
                    _context.TrolleyItems.Remove(trolleyItem); // and if it's 1, remove item
                }
                
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Trolley> GetTrolleyAsync()
        {
            var trolleyItems = await _context.TrolleyItems.Include(ti => ti.Beer).ToListAsync();
            return new Trolley { Items = trolleyItems };
        }

    }
}

