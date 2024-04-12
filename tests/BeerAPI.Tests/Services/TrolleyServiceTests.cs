using BeerAPI.Models;
using BeerAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerAPI.Tests.Services
{
    public class TrolleyServiceTests
    {
        private readonly TrolleyService _trolleyService;

        public TrolleyServiceTests()
        {
            _trolleyService = new TrolleyService();
        }

        [Fact]
        public void AddItem_ShouldAddToTrolley()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };

            // Act
            _trolleyService.AddItem(beer);

            // Assert
            Assert.Equal(1, _trolleyService.GetItemCount());
        }

        [Fact]
        public void AddItem_ShouldIncreaseQuantity()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };

            // Act
            _trolleyService.AddItem(beer);
            _trolleyService.AddItem(beer);

            // Assert
            Assert.Equal(2, _trolleyService.GetItemCount());
        }

        [Fact]
        public void RemoveItem_ShouldDecreaseQuantity()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };
            _trolleyService.AddItem(beer);
            _trolleyService.AddItem(beer); 

            // Act
            _trolleyService.RemoveItem(1);

            // Assert
            var trolley = _trolleyService.GetTrolley();
            var trolleyItem = trolley.Items.FirstOrDefault(i => i.Beer.Id == beer.Id);
            Assert.NotNull(trolleyItem);
            Assert.Equal(1, trolleyItem.Quantity); 
        }


        [Fact]
        public void RemoveItem_BeerDoesNotExist_ReturnsFalse()
        {
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
