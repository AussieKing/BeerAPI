using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models;
using BeerAPI.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeerById(int id)
        {
            var beer = await _beerService.GetBeerByIdAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeer(Beer beer)
        {
            await _beerService.AddBeerAsync(beer);
            return CreatedAtAction(nameof(GetBeerById), new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            var existingBeer = await _beerService.GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                return NotFound();
            }

            try
            {
                await _beerService.UpdateBeerAsync(beer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BeerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var existingBeer = await _beerService.GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                return NotFound();
            }

            await _beerService.DeleteBeerAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/promoprice")]
        public async Task<IActionResult> UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
            var beer = await _beerService.GetBeerByIdAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            beer.PromoPrice = request.NewPromoPrice;
            await _beerService.UpdateBeerAsync(beer);

            return NoContent();
        }

        private async Task<bool> BeerExists(int id)
        {
            var beer = await _beerService.GetBeerByIdAsync(id);
            return beer != null;
        }
    }

    public class PromoPriceUpdateRequest
    {
        public decimal? NewPromoPrice { get; set; }
    }
}
