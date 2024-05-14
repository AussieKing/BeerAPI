using BeerAPI.Controllers;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BeerAPI.Repositories;

namespace BeerAPI.Tests.Controllers
{
    public class TrolleyControllerTests
    {
        private readonly Mock<IBeerService> _mockBeerService;
        private readonly Mock<ITrolleyService> _mockTrolleyService;
        private readonly TrolleyController _controller;

        public TrolleyControllerTests()
        {
            _mockBeerService = new Mock<IBeerService>();
            _mockTrolleyService = new Mock<ITrolleyService>();
            _controller = new TrolleyController(_mockTrolleyService.Object, _mockBeerService.Object);
        }

        [Fact]
        public async Task AddItemToTrolley_NonExistingBeerId_ReturnsNotFound()
        {
            // Arrange
            _mockBeerService.Setup(service => service.GetBeerByIdAsync(It.IsAny<int>())).ReturnsAsync((Beer)null);

            // Act
            var result = await _controller.AddItemToTrolley(999); // this ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RemoveItemFromTrolley_NonExistingBeerId_ReturnsNotFound()
        {
            // Arrange
            _mockTrolleyService.Setup(service => service.RemoveItemAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.RemoveItemFromTrolley(999); // this ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
