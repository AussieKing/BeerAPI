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
        public async Task<IActionResult> AddBeer([FromBody]Beer beer)
        {
            if (beer == null)
            {
                return BadRequest();  // making sure no null obj are added
            }

            beer.Id = 0; // ensuring the db creates new Id

            var createdBeer = await _beerService.AddBeerAsync(beer);
            return CreatedAtAction(nameof(GetBeerById), new { id = createdBeer.Id }, createdBeer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, UpdateBeerRequest updateBeerRequest)
        {
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
            catch (Exception) 
            {
                return StatusCode(500); // Status 500
            }
        }

        [HttpPatch("{id}/promoprice")]
        public async Task<IActionResult> UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
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
}
