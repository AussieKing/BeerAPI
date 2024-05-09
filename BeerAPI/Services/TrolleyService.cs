using BeerAPI.Models;
using BeerAPI.Repositories;
using System;
using System.Linq;

namespace BeerAPI.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly ITrolleyRepository _trolleyRepository; 

        // Constructor injection of the repo
        public TrolleyService(ITrolleyRepository trolleyRepository)
        {
            _trolleyRepository = trolleyRepository;
        }

        public TrolleyService()
        {
        }

        // ADD
        public void AddItem(Beer beer)
        {
            _trolleyRepository.AddBeerToTrolley(beer);
            PrintTrolleyState();
        }

        // REMOVE
        public bool RemoveItem(int beerId)
        {
            return _trolleyRepository.RemoveBeerFromTrolley(beerId);
        }
/*
        public int GetItemCount() => _trolleyRepository.Items.Sum(i => i.Quantity);*/

        // GET
        public Trolley GetTrolley()
        {
            return _trolleyRepository.GetTrolley();
        }

        // Logic to print the state of the trolley
        private void PrintTrolleyState()
        {
            var trolley = GetTrolley(); // get the trolley from the repository
            Console.WriteLine($"Current trolley count: {trolley.Items.Sum(i => i.Quantity)}");
            foreach (var item in trolley.Items)
            {
                Console.WriteLine($"Item: {item.Beer?.Name}, Quantity: {item.Quantity}");
            }

        }
    }
}
