using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models;
using BeerAPI.Services;
using System.Linq; 

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyService _trolleyService;
        private readonly IBeerService _beerService;

        public TrolleyController(ITrolleyService trolleyService, IBeerService beerService)
        {
            _trolleyService = trolleyService;
            _beerService = beerService;
        }

        [HttpPost("{beerId}")]
        public async Task<IActionResult> AddItemToTrolley(int beerId) 
        {
            var beer = await _beerService.GetBeerByIdAsync(beerId);
            if (beer == null)
            {
                return NotFound($"Beer with ID {beerId} not found.");
            }

            await _trolleyService.AddItemAsync(beer);
            return Ok(new { message = "Item added to trolley.", trolley = _trolleyService.GetTrolleyAsync() });
        }

        [HttpDelete("{beerId}")]
        public async Task<IActionResult> RemoveItemFromTrolley(int beerId)
        {
            bool itemRemoved = await _trolleyService.RemoveItemAsync(beerId);
            if (!itemRemoved)
            {
                return NotFound(new {message = "Trolley is empty or item not found!" });
            }
            var trolley = await _trolleyService.GetTrolleyAsync();
            return Ok(new { message = "Item removed from trolley.", trolley });
        }

        [HttpGet]
        public async Task<IActionResult> GetTrolley()
        {
            var trolley = await _trolleyService.GetTrolleyAsync();
            return Ok(trolley);
        }
    }
}
