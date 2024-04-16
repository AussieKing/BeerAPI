using BeerAPI.Models;

namespace BeerAPI.Services
{
    public class BeerService : IBeerService
    {
        private List<Beer> beers = new List<Beer>
        {
                new Beer { Id = 1, Name = "Lager", Price = 5.99M },
                new Beer { Id = 2, Name = "IPA", Price = 10.00M },
                new Beer { Id = 3, Name = "Pale Ale", Price = 6.50M },
                new Beer { Id = 4, Name = "Pilsner", Price = 6.50M },
                new Beer { Id = 5, Name = "Sour", Price = 9.99M }
           };

        public Beer? GetBeerById(int beerId) => beers.FirstOrDefault(b => b.Id == beerId);

        public List<Beer> GetAllBeers() => beers;

        public Beer AddBeer(Beer newBeer)
        {
            var nextId = beers.Max(b => b.Id) + 1;
            newBeer.Id = nextId;
            beers.Add(newBeer);
            return newBeer;
        }

        public bool DeleteBeer(int id)
        {
            var beer = GetBeerById(id);
            if (beer != null)
            {
                beers.Remove(beer);
                return true;
            }
            return false;
        }

        public void UpdateBeer(Beer updatedBeer)
        {
            var beer = GetBeerById(updatedBeer.Id);
            if (beer != null)
            {
                beer.Name = updatedBeer.Name;
                beer.Price = updatedBeer.Price;
                beer.PromoPrice = updatedBeer.PromoPrice;
            }
        }

    }
}