using BeerAPI.Models;
using BeerAPI.Services;
using BeerAPI.Data;
using BeerAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using BeerAPI.Data.Repositories;

namespace BeerAPI.Tests.Services
{
    public class BeerServiceTests : IDisposable
    {
        private readonly BeerService _service;
        private readonly ApplicationDbContext _context;

        public BeerServiceTests()
        {
            // Setting up an in-memory database with EF Core
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BeerAPI_Test")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            _service = new BeerService(new BeerRepository(_context));
        }

        [Fact]
        public async Task AddBeer_ShouldBeInList()
        {
            // Arrange
            var newBeer = new Beer { Name = "Test Beer", Price = 5.99M };

            // Act
            var addedBeer = await _service.AddBeerAsync(newBeer);

            // Assert
            var beers = await _service.GetAllBeersAsync();
            Assert.Contains(beers, b => b.Id == addedBeer.Id);
            Assert.Equal("Test Beer", addedBeer.Name);
            Assert.True(addedBeer.Id > 0);
        }

        [Fact]
        public async Task GetBeerById_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = 999; // ID that does not exist

            // Act
            var beer = await _service.GetBeerByIdAsync(nonExistingId);

            // Assert
            Assert.Null(beer);
        }

        [Fact]
        public async Task DeleteBeer_ExistingId_ShouldRemoveFromList()
        {
            // Arrange
            var beer = new Beer { Name = "Test Beer", Price = 5.99M };
            var addedBeer = await _service.AddBeerAsync(beer);

            // Act
            var deleteResult = await _service.DeleteBeerAsync(addedBeer.Id);
            var beerAfterDelete = await _service.GetBeerByIdAsync(addedBeer.Id);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(beerAfterDelete);
        }

        [Fact]
        public async Task UpdateBeer_ExistingId_UpdatesBeer()
        {
            // Arrange
            var beer = new Beer { Name = "Test Beer", Price = 5.99M };
            var addedBeer = await _service.AddBeerAsync(beer);

            var updatedBeer = new Beer { Id = addedBeer.Id, Name = "Updated Test Beer", Price = 6.99M, PromoPrice = 4.99M };

            // Act
            await _service.UpdateBeerAsync(updatedBeer);

            // Assert
            var resultBeer = await _service.GetBeerByIdAsync(addedBeer.Id);
            Assert.NotNull(resultBeer);
            Assert.Equal("Updated Test Beer", resultBeer.Name);
            Assert.Equal(6.99M, resultBeer.Price);
            Assert.Equal(4.99M, resultBeer.PromoPrice);
        }

        public void Dispose()
        {
            // Cleanup in-memory database
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
