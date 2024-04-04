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

        private IBeerDescriptionService _beerDescriptionService;

        //! INJECTION
        public BeersController(IBeerDescriptionService beerDescriptionService)
        {
            _beerDescriptionService = beerDescriptionService; 
        }

        // GET REQUEST: return of info given a BeerId
        [HttpGet("{id}")] 
        public ActionResult<Beer> GetBeerById(int id) 
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
        [HttpPost] 
        public ActionResult<Beer> AddBeer(Beer newBeer) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            newBeer.Id = beers.Count + 1;
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
