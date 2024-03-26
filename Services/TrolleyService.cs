using BeerAPI.Models;
using System.Linq;

namespace BeerAPI.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly Trolley _trolley = new Trolley();

        public void AddItem(Beer beer)
        {
            var item = _trolley.Items.FirstOrDefault(i  => i.Beer?.Id == beer.Id);
            if (item == null)
            {
                _trolley.Items.Add(new TrolleyItem { Beer = beer, Quantity = i });
            }
            else
            {
                item.Quantity++;
            }
        }

        public void RemoveItem(int beerId) 
        {
            var item = _trolley.Items.FirstOrDefault(i = i.Beer?.Id == beerId);
            if (item != null)
            {
                item.Quantity--;
                _trolley.Items.Remove(item);
            }
        }

        public int GetItemCount()
        {
            return _trolley.Items.Sum(i => i.Quantity);
        }

        public Trolley GetTrolley()
        {
            return _trolley;
        }

    }
}
