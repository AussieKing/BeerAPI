using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models;
using BeerAPI.Data;
using System.Threading.Tasks;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeer([FromBody] Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBeer), new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.Entry(beer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
