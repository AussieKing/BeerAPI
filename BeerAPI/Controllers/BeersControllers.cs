using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models;
using BeerAPI.Services;
using System.Threading.Tasks;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly IBeerDescriptionService _beerDescriptionService;

        public BeersController(IBeerService beerService, IBeerDescriptionService beerDescriptionService)
        {
            _beerService = beerService;
            _beerDescriptionService = beerDescriptionService;
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
        public async Task<IActionResult> AddBeer([FromBody] Beer beer)
        {
            if (beer == null)
            {
                return BadRequest();
            }
            var newBeer = await _beerService.AddBeerAsync(beer);
            return CreatedAtAction(nameof(GetBeerById), new { id = newBeer.Id }, newBeer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _beerService.GetBeerByIdAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            await _beerService.DeleteBeerAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] Beer beer)
        {
            if (beer == null || beer.Id != id)
            {
                return BadRequest();
            }
            var existingBeer = await _beerService.GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                return NotFound();
            }
            await _beerService.UpdateBeerAsync(beer);
            return NoContent();
        }

        [HttpPatch("{id}/promoprice")]
        public async Task<IActionResult> UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
            if (request == null || request.NewPromoPrice < 0)
            {
                return BadRequest();
            }
            var beer = await _beerService.GetBeerByIdAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            beer.PromoPrice = request.NewPromoPrice;
            await _beerService.UpdateBeerAsync(beer);
            return NoContent();
        }
    }
}
