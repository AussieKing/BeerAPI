using BeerAPI.Models;
using BeerAPI.Services;
using Xunit;


namespace BeerAPI.Tests.Services
{
    public class TrolleyServiceTests
    {
        private TrolleyService _trolleyService;

        public TrolleyServiceTests()
        {
            _trolleyService = new TrolleyService();
        }

        private void ResetTrolleyServiceState()
        {
            _trolleyService = new TrolleyService(); // adding this to re-set the state of the service before each test
        }

        [Fact]
        public void AddItem_ShouldAddToTrolley()
        {
            ResetTrolleyServiceState();

            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };

            // Act
            _trolleyService.AddItem(beer);
            var addedItem = _trolleyService.GetTrolley().Items.FirstOrDefault();

            // Assert
            Assert.NotNull(addedItem);
            Assert.Equal(beer.Name, addedItem.Beer.Name);
            Assert.Equal(beer.Price, addedItem.Beer.Price);
        }

        [Fact]
        public void AddItem_ShouldIncreaseQuantity()
        {
            ResetTrolleyServiceState();

            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };

            // Act
            _trolleyService.AddItem(beer);
            _trolleyService.AddItem(beer);

            var trolleyItem = _trolleyService.GetTrolley().Items.FirstOrDefault(i => i.Beer.Id == beer.Id);

            // Assert
            Assert.NotNull(trolleyItem);
            Assert.Equal(2, trolleyItem.Quantity);
        }

        [Fact]
        public void RemoveItem_ShouldDecreaseQuantity()
        {
            ResetTrolleyServiceState();

            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };
            _trolleyService.AddItem(beer);
            _trolleyService.AddItem(beer); 

            // Act
            _trolleyService.RemoveItem(beer.Id);

            // Assert
            var trolleyItem = _trolleyService.GetTrolley().Items.FirstOrDefault(i => i.Beer.Id == beer.Id);
            Assert.NotNull(trolleyItem);
            Assert.Equal(1, trolleyItem.Quantity); 
        }


        [Fact]
        public void RemoveItem_BeerDoesNotExist_ReturnsFalse()
        {
            ResetTrolleyServiceState();

            // Arrange
            var beerId = 999; // not existing id

            // Act
            var removeResult = _trolleyService.RemoveItem(beerId);

            // Assert
            Assert.False(removeResult);
        }

        [Fact]
        public void GetItemCount_ItemsAdded_ReturnsCorrectCount()
        {
            ResetTrolleyServiceState();

            // Arrange
            _trolleyService.AddItem(new Beer { Id = 1, Name = "Beer One", Price = 2.50M });
            _trolleyService.AddItem(new Beer { Id = 2, Name = "Beer Two", Price = 3.50M });

            // Act
            var count = _trolleyService.GetItemCount();

            // Assert
            Assert.Equal(2, count);
        }

    }
}
