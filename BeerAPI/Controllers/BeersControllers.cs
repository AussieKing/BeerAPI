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
            if (beer == null)
            {
                return BadRequest();  // making sure no null obj are added
            }

            var createdBeer = await _beerService.AddBeerAsync(beer);
            return CreatedAtAction(nameof(GetBeerById), new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, UpdateBeerRequest updateBeerRequest)
        {
            /*var existingBeer = await _beerService.GetBeerByIdAsync(id);
            if (existingBeer == null)
            {
                return NotFound();
            }

            existingBeer.Name = updateBeerRequest.Name;
            existingBeer.Price = updateBeerRequest.Price;
            existingBeer.PromoPrice = updateBeerRequest.PromoPrice;

            try
            {
                await _beerService.UpdateBeerAsync(existingBeer);
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
            catch (Exception)
            {
                return StatusCode(500, "Error occurred when updating the beer");
            }
            return NoContent();*/
            if (updateBeerRequest == null)
            {
                return BadRequest();
            }
            
            try
            {
                await _beerService.UpdateBeerAsync(id, updateBeerRequest);
                return NoContent();
            }
            catch (NotFoundException) 
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            try
            {
                await _beerService.DeleteBeerAsync(id);
                return NoContent(); // Status 200
            }
            catch (NotFoundException)
            {
                return NotFound(); // Status 404
            }
            catch (Exception ex) 
            {
                return StatusCode(500); // Status 500
            }
        }

        [HttpPatch("{id}/promoprice")]
        public async Task<IActionResult> UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
            /*var beer = await _beerService.GetBeerByIdAsync(id);
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
        }*/
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                await _beerService.UpdatePromoPriceAsync(id, request.NewPromoPrice);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }

    public class PromoPriceUpdateRequest
    {
        public decimal? NewPromoPrice { get; set; }
    }
}
