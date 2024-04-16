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
        public IActionResult AddItemToTrolley(int beerId)
        {
            var beer = _beerService.GetBeerById(beerId);
            if (beer == null)
            {
                return NotFound($"Beer with ID {beerId} not found.");
            }

            _trolleyService.AddItem(beer);
            return Ok(new { message = "Item added to trolley.", trolley = _trolleyService.GetTrolley() });
        }

        [HttpDelete("{beerId}")]
        public IActionResult RemoveItemFromTrolley(int beerId)
        {
            bool itemRemoved = _trolleyService.RemoveItem(beerId);
            if (!itemRemoved)
            {
                return NotFound(new {message = "Trolley is empty or item not found!" });
            }
            return Ok(new { message = "Item removed from trolley.", trolley = _trolleyService.GetTrolley() });
        }

        [HttpGet]
        public IActionResult GetTrolley()
        {
            var trolley = _trolleyService.GetTrolley();
            return Ok(trolley);
        }
    }
}
