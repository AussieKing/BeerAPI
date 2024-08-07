﻿using BeerAPI.Models;
using BeerAPI.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System;

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
        public async Task AddItemAsync(Beer beer)
        {
            await _trolleyRepository.AddBeerToTrolleyAsync(beer);
            await PrintTrolleyStateAsync();
        }

        // REMOVE
        public async Task<bool> RemoveItemAsync(int beerId)
        {
            var result = await _trolleyRepository.RemoveBeerFromTrolleyAsync(beerId);
            await PrintTrolleyStateAsync();
            return result;
        }

        // GET the current state of the trolley
        public async Task<Trolley> GetTrolleyAsync()
        {
            var trolley = await _trolleyRepository.GetTrolleyAsync();
            Console.WriteLine($"Current trolley count: {trolley.Items.Sum(i => i.Quantity)}");
            foreach (var item in trolley.Items)
            {
                Console.WriteLine($"Beer ID: {item.Beer.Id}, Name: {item.Beer.Name}, Quantity: {item.Quantity}");
            }
            return trolley;
        }

        private async Task PrintTrolleyStateAsync()
        {
            var trolley = await GetTrolleyAsync();
            foreach (var item in trolley.Items)
            {
                Console.WriteLine($"Item: {item.Beer?.Name}, Quantity: {item.Quantity}");
            }
        }
    }
}
