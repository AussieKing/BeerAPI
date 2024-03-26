using BeerAPI.Models;

namespace BeerAPI.Services
{
    public interface ITrolleyService
    {
        void AddItem(Beer beer);
        void RemoveItem(int beerId);
        int GetItemCount();
        Trolley GetTrolley();
    }
}
