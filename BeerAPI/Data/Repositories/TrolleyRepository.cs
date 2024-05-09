using BeerAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeerAPI.Repositories
{
    public class TrolleyRepository : ITrolleyRepository
    {
        private static readonly List<TrolleyItem> _items = new List<TrolleyItem>();

        public object Items => throw new NotImplementedException();

        public void AddBeerToTrolley(Beer beer)
        {
            var item = _items.FirstOrDefault(i => i.Beer.Id == beer.Id);
            if (item == null)
            {
                _items.Add(new TrolleyItem { Beer = beer, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }
        }

        public bool RemoveBeerFromTrolley(int beerId)
        {
            var item = _items.FirstOrDefault(i => i.Beer.Id == beerId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity == 0)
                {
                    _items.Remove(item);
                }
                return true;
            }
            return false;
        }

        public Trolley GetTrolley()
        {
            return new Trolley { Items = _items };
        }
    }
}
