using BeerAPI.Models;
using BeerAPI.Repositories;
using System.Linq;

namespace BeerAPI.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly ITrolleyRepository _trolleyRepository;

        // Constructor injection of the repository
        public TrolleyService(ITrolleyRepository trolleyRepository)
        {
            _trolleyRepository = trolleyRepository;
        }

        // ADD
        public void AddItem(Beer beer)
        {
            _trolleyRepository.AddBeerToTrolley(beer);
            PrintTrolleyState();  // Call to print the current state after adding an item
        }

        // REMOVE
        public bool RemoveItem(int beerId)
        {
            bool result = _trolleyRepository.RemoveBeerFromTrolley(beerId);
            PrintTrolleyState();  // Call to print the current state after removing an item
            return result;
        }

        // GET the current state of the trolley
        public Trolley GetTrolley()
        {
            var trolley = _trolleyRepository.GetTrolley();  // Fetch the trolley from the repository

            // Log each item in the trolley for debugging
            Console.WriteLine($"Current trolley count: {trolley.Items.Sum(i => i.Quantity)}");
            foreach (var item in trolley.Items)
            {
                Console.WriteLine($"Beer ID: {item.Beer.Id}, Name: {item.Beer.Name}, Quantity: {item.Quantity}");
            }

            return trolley;
        }
        private void PrintTrolleyState()
        {
            var trolley = GetTrolley();  // Fetch the trolley again
            foreach (var item in trolley.Items)
            {
                Console.WriteLine($"Item: {item.Beer?.Name}, Quantity: {item.Quantity}");
            }
        }
    }
}

