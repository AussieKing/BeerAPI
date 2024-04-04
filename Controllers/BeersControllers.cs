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
        private static List<Beer> beers = new List<Beer>  
        {
            new Beer { Id = 1, Name = "Lager", Price = 5.99M },
            new Beer { Id = 2, Name = "IPA", Price = 6.49M, PromoPrice = 3.99M }
        };

        private static int _highestBeerId = beers.Max(b => b.Id);

        private IBeerDescriptionService _beerDescriptionService;

        //! INJECTION
        public BeersController(IBeerDescriptionService beerDescriptionService)
        {
            _beerDescriptionService = beerDescriptionService; 
        }

        // GET REQUEST: return of info given a BeerId
        [HttpGet("{id}")] 
        public IActionResult GetBeerById(int id) 
        {
            var beer = beers.FirstOrDefault(b => b.Id == id); 
            if (beer == null) 
            {
                return NotFound(); 
            }

            var description = ((IBeerDescriptionService)_beerDescriptionService).GetDescription(beer);
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

            _highestBeerId++;
            newBeer.Id = _highestBeerId;
            beers.Add(newBeer);

            return CreatedAtAction(nameof(GetBeerById), new { id = newBeer.Id }, newBeer);
        }

        // DELETE REQUEST: remove a beer from the list.
        [HttpDelete("{id}")] 
        public ActionResult DeleteBeer(int id)
        {
            var beer = beers.FirstOrDefault(b => b.Id == id); 
            if (beer == null) 
            {
                return NotFound(); 
            }

            beers.Remove(beer); 
            return NoContent(); 
        }

        // PUT REQUEST: update a beer's promo price, and its promo cannot be less than half of the normal price.
        [HttpPut("{id}/promo-price")] 
        public ActionResult UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        {
            var beer = beers.FirstOrDefault(b => b.Id == id);  
            if (beer == null) 
            {
                return NotFound();
            }

            if (request.NewPromoPrice < beer.Price / 2) 
            {
                return BadRequest("Promotional price cannot be less than half of the normal price."); 
            }

            beer.PromoPrice = request.NewPromoPrice;
            return NoContent(); 
        }
    }
}
