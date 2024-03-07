using Microsoft.AspNetCore.Mvc;
using BeerAPI.Models; // import the Beer class from the Models namespace
using BeerAPI.Services; //! Adding this to use the BeerDescriptionService

//? Step 1: CONTROLLER DECLARATION
// Defining the BeersControllers inside the 'BeerAPI.Controllers'
namespace BeerAPI.Controllers
{
    [ApiController]  // declaring it's an API class
    [Route("api/[controller]")]  // declaring the URL route as /api/beers -> from the controller name (controller name 'BeersController' minus the word 'Controller' at the end)

    //? Step 2: DATA STORE
    public class BeersController : ControllerBase  // we are saying that BeersController inherits from ControllerBase (which is a native class that provides a lot of functionality for handling HTTP requests)
    {
        // this is just initializing a static (dummy) list of Beer objects (I don't have a database of beers handy! (yet)
        private static List<Beer> beers = new List<Beer>  // taking the Beer class and creating a list of Beer objects
        {
            new Beer { Id = 1, Name = "Lager", Price = 5.99M }, // using the M at the end to indicate that the number is a decimal (more precise than double)
            new Beer { Id = 2, Name = "IPA", Price = 6.49M, PromoPrice = 3.99M }
        };
        private object _beerDescriptionService;

        //! NEW CONSTRUCTOR INJECTION
        public BeersController(IBeerDescriptionService beerDescriptionService)
        {
            _beerDescriptionService = beerDescriptionService; // Assigning the injected service to a private field
        }


        //? Step 3: CRUD requirements 
        // GET REQUEST
        //First operation is the return of info given a BeerId
        [HttpGet("{id}")] // we are telling the framework that this method will be called when an HTTP GET request is made to the /api/beers/{id}
        public ActionResult<Beer> GetBeerById(int id) // here ActionResult will return a <Beer> object, and the method is called GetBeerById (the parameter is the id of the beer, which is an integer)
        {
            var beer = beers.FirstOrDefault(b => b.Id == id); // we are using the FirstOrDefault method to find the first beer in the list that has the same id as the one passed as a parameter
            if (beer == null) // if the beer is not found, we return a 404 Not Found status code
            {
                return NotFound(); // NotFound returns 404
            }

            //! Using the _beerDescriptionService to get the description
            var description = ((IBeerDescriptionService)_beerDescriptionService).GetDescription(beer); // Cast _beerDescriptionService to IBeerDescriptionService and call GetDescription method
            return Ok(new { Beer = beer, Description = description }); // Return the beer and its description
        }        

        // POST REQUEST
        // Second operation is to add a new beer to the list. In order to do this, we need to accept the beer data and then add it to the list
        [HttpPost] // similarly to above, we're saying that this method will be called when an HTTP POST request is made to the /api/beers URL
        public ActionResult<Beer> AddBeer(Beer newBeer) // ActionResult returns <Beer> again, but the method here is AddBeer, and it take the Beer object, and the newBeer is the parameter
        {
            // because we need to check if all the required fields are there, we need to to check if the model is valid
            if (!ModelState.IsValid) // if the model is not valid, we return a 400 Bad Request status code
            {
                return BadRequest(ModelState); // BadRequest returns 400 error
            }

            // if the model is valid, we add the new beer to the list
            newBeer.Id = beers.Count + 1; // we are setting the id of the new beer to be the count of the beers in the list + 1
            beers.Add(newBeer); // we are adding the new beer to the list

            return CreatedAtAction(nameof(GetBeerById), new { id = newBeer.Id }, newBeer); // we are returning a 201 Created status code, and we are also returning the new beer!
        }

        // DELETE REQUEST
        // Third operation is to remove a beer from the list. In order to do this, we need to accept the beer id and then remove it from the list
        [HttpDelete("{id}")] // we are saying that this method will be called when an HTTP DELETE request is made to the /api/beers/{id} URL
        public ActionResult DeleteBeer(int id) // we are returning an ActionResult, and the method is called DeleteBeer, and the parameter is the id of the beer
        {
            var beer = beers.FirstOrDefault(b => b.Id == id); // we are using the FirstOrDefault method again to find the first beer in the list that has the same id as the one passed as a parameter
            if (beer == null) // if the beer is not found, we return a 404 Not Found status code
            {
                return NotFound(); // NotFound returns 404 error
            }

            beers.Remove(beer); // if the beer is found, we remove it from the list
            return NoContent(); // we return a 204 No Content status code
        }

        // PUT REQUEST
        // Fourth operation is to update a beer's promo price, and its promo cannot be less than half of the normal price.
        [HttpPut("{id}/promo-price")]  // we are stating that this method is invoked with a PUT request, at the /api/beers/{id}/promo-price URL
        public ActionResult UpdatePromoPrice(int id, [FromBody] PromoPriceUpdateRequest request)
        // UpdatePromoPrice takes two parameters: the id of the beer (integer), and the new promotional price (decimal)
        {
            var beer = beers.FirstOrDefault(b => b.Id == id);  // here we search for the first beer in the list that has the id we passed
            if (beer == null) // if the beer is not found (null), we return a 404 Not found error
            {
                return NotFound();
            }

            if (request.NewPromoPrice < beer.Price / 2) // we also have to check if the new promotional price is less than half of the normal price
            {
                return BadRequest("Promotional price cannot be less than half of the normal price."); // if it's less than half, we return a 400 Bad Request error
            }

            beer.PromoPrice = request.NewPromoPrice; // if the new promotional price is valid, we update the beer's promotional price
            return NoContent(); // and we return a 204 code (no content to return, but the operation was successful)
        }
    }
}
