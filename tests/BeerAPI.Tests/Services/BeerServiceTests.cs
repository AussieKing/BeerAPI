using BeerAPI.Models;
using BeerAPI.Services;

namespace BeerAPI.Tests.Services
{
    public class BeerServiceTests
    {
        private readonly BeerService _service;

        public BeerServiceTests()
        {
            _service = new BeerService();
        }

        [Fact]
        public void AddBeer_ShouldBeInList()
        {
            // Arrange
            var newBeer = new Beer { Name = "Test Beer", Price = 5.99M };

            // Act
            var addedBeer = _service.AddBeer(newBeer);

            // Assert
            Assert.Contains(addedBeer, _service.GetAllBeers());
            Assert.Equal("Test Beer", addedBeer.Name);
            // Ensure that an ID has been assigned
            Assert.True(addedBeer.Id > 0);
        }

        [Fact]
        public void GetBeerById_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = 999; // ID that does not exist in the initial list

            // Act
            var beer = _service.GetBeerById(nonExistingId);

            // Assert
            Assert.Null(beer);
        }

        [Fact]
        public void DeleteBeer_ExistingId_ShouldRemoveFromList()
        {
            // Arrange
            var beerIdToDelete = 1; // this ID exists in my initial list

            // Act
            var deleteResult = _service.DeleteBeer(beerIdToDelete);
            var beerAfterDelete = _service.GetBeerById(beerIdToDelete);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(beerAfterDelete);
        }
    }
}
