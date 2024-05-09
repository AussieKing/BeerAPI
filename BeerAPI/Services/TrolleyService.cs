using BeerAPI.Models;

namespace BeerAPI.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly Trolley _trolley = new Trolley();  // using _ to denote the fact it's a private field of a class.

        public void AddItem(Beer beer)
        {
            var item = _trolley.Items.FirstOrDefault(i => i.Beer?.Id == beer.Id);
            if (item == null)
            {
                _trolley.Items.Add(new TrolleyItem { Beer = beer, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }
            PrintTrolleyState();
        }

        public bool RemoveItem(int beerId)
        {
            var item = _trolley.Items.FirstOrDefault(i => i.Beer?.Id == beerId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    _trolley.Items.Remove(item);
                }
                return true;
            }
            return false;
        }

        public int GetItemCount()
        {
            return _trolley.Items.Sum(i => i.Quantity);
        }

        public Trolley GetTrolley()
        {
            return _trolley;
        }

        // Logic to print the state of the trolley
        private void PrintTrolleyState()
        {
            Console.WriteLine($"Current trolley count: {GetItemCount()}");
            foreach (var item in _trolley.Items)
            {
                Console.WriteLine($"Item: {item.Beer?.Name}, Quantity: {item.Quantity}");
            }

        }
    }
}
