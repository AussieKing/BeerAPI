using BeerAPI.Controllers;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System.Linq;
using BeerAPI.Repositories;

namespace BeerAPI.Tests.Controllers
{
    public class TrolleyControllerTests
    {
        private readonly TrolleyService _trolleyService1;
        private readonly TrolleyService _trolleyService2;
        private Mock<ITrolleyRepository> _mockRepository1;
        private Mock<ITrolleyRepository> _mockRepository2;

        public TrolleyControllerTests()
        {
            _mockRepository1 = new Mock<ITrolleyRepository>();
            _mockRepository2 = new Mock<ITrolleyRepository>();
            _trolleyService1 = new TrolleyService(_mockRepository1.Object);
            _trolleyService2 = new TrolleyService(_mockRepository2.Object);
        }

        [Fact]
        public void AddItemToTrolley_ShouldHandleMultipleInstancesSeparately()
        {
            // Act
            _trolleyService1.AddItem(new Beer { Id = 1, Name = "Test Beer1", Price = 6.99M });
            _trolleyService2.AddItem(new Beer { Id = 2, Name = "Test Beer2", Price = 7.99M });

            // Assert
            Assert.Single(_trolleyService1.GetTrolley().Items);
            Assert.Single(_trolleyService2.GetTrolley().Items);
            Assert.NotEqual(_trolleyService1.GetTrolley().Items.First().Beer.Id, _trolleyService2.GetTrolley().Items.First().Beer.Id);
        }

        [Fact]
        public void RemoveItemFromTrolley_ShouldReflectIndividualUserChanges()
        {
            // Arrange
            _trolleyService1.AddItem(new Beer { Id = 1, Name = "Test Beer", Price = 6.99M });
            _trolleyService2.AddItem(new Beer { Id = 2, Name = "Test Beer", Price = 7.99M });

            // Act
            var removeResultUser1 = _trolleyService1.RemoveItem(1);

            // Assert
            Assert.True(removeResultUser1);
            Assert.Empty(_trolleyService1.GetTrolley().Items);
            Assert.Single(_trolleyService2.GetTrolley().Items);
        }
    }
}
