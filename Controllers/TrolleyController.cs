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

        [HttpPost("add/{beerId}")]
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

        [HttpPost("remove/{beerId}")]
        public IActionResult RemoveItemFromTrolley(int beerId)
        {
            _trolleyService.RemoveItem(beerId);
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
