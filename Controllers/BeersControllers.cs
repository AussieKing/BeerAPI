using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models; 
using BeerAPI.Services; 

namespace BeerAPI.Controllers
{
    [ApiController]  
    [Route("api/[controller]")] 

    //? Step 2: DATA STORE
    public class BeersController : ControllerBase 
    {
        private readonly IBeerService _beerService;
        private readonly IBeerDescriptionService _beerDescriptionService;

        //! INJECTION
        public BeersController(IBeerService beerService, IBeerDescriptionService beerDescriptionService)
        {
            _beerService = beerService;
            _beerDescriptionService = beerDescriptionService;
        }

        // GET REQUEST: return of info given a BeerId
        [HttpGet("{id}")] 
        public IActionResult GetBeerById(int id) 
        {
            var beer = _beerService.GetBeerById(id); 
            if (beer == null) 
            {
                return NotFound(); 
            }

            var description = _beerDescriptionService.GetDescription(beer);
            return Ok(new { Beer = beer, Description = description }); 
        }        

        // POST REQUEST: add a new beer to the list. 
        // changed it so that if a beer is deleted , the next beer added will have an id that is one higher than the highest beer id.
        [HttpPost] 
        public ActionResult<Beer> AddBeer(Beer newBeer) 
        {
           if (!ModelState.IsValid)
            {
               return BadRequest(ModelState); 
           }

            var addedBeer = _beerService.AddBeer(newBeer);
            return CreatedAtAction(nameof(GetBeerById), new { id = addedBeer.Id }, addedBeer);
        }

        // DELETE REQUEST: remove a beer from the list.
        [HttpDelete("{id}")] 
        public ActionResult DeleteBeer(int id)
        {
            var success = _beerService.DeleteBeer(id); 
            if (!success) 
            {
                return NotFound(); 
            }
            
            return NoContent(); 
        }

        // PUT REQUEST: update a beer's promo price, and its promo cannot be less than half of the normal price.
        [HttpPut("{id}/promo-price")] 
        public IActionResult UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
            var beer = _beerService.GetBeerById(id);
            if (beer == null)
            {
                return NotFound();
            }

            if (request.NewPromoPrice < beer.Price / 2) 
            {
                return BadRequest("Promotional price cannot be less than half of the normal price."); 
            }
            return NoContent(); 
        }
    }
}
