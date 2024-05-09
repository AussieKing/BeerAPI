using BeerAPI.Models;

namespace BeerAPI.Services
{
    public interface ITrolleyService
    {
        void AddItem(Beer beer);
        bool RemoveItem(int beerId);
/*        int GetItemCount();
*/        Trolley GetTrolley();
    }
}
