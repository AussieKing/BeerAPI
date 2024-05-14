using BeerAPI.Models;
using BeerAPI.Repositories;
using BeerAPI.Services;
using Xunit;
using Moq;
using System.Linq;

namespace BeerAPI.Tests.Services
{
    public class TrolleyServiceTests
    {
        private readonly TrolleyService _trolleyService;
        private readonly Mock<ITrolleyRepository> _mockTrolleyRepository;

        public TrolleyServiceTests()
        {
            _mockTrolleyRepository = new Mock<ITrolleyRepository>();
            _trolleyService = new TrolleyService(_mockTrolleyRepository.Object);
        }

        [Fact]
        public async Task AddItem_ShouldAddToTrolley()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };
            _mockTrolleyRepository.Setup(r => r.GetTrolleyAsync()).ReturnsAsync(new Trolley());

            // Act
            await _trolleyService.AddItemAsync(beer);

            // Assert
            _mockTrolleyRepository.Verify(r => r.AddBeerToTrolleyAsync(It.IsAny<Beer>()), Times.Once);
        }

        [Fact]
        public async Task RemoveItem_ShouldDecreaseQuantity()
        {
            // Arrange
            var beerId = 1;
            _mockTrolleyRepository.Setup(r => r.RemoveBeerFromTrolleyAsync(beerId)).ReturnsAsync(true);

            // Act
            var result = await _trolleyService.RemoveItemAsync(beerId);

            // Assert
            Assert.True(result);
            _mockTrolleyRepository.Verify(r => r.RemoveBeerFromTrolleyAsync(beerId), Times.Once);
        }

        [Fact]
        public async Task GetTrolley_ReturnsTrolley()
        {
            // Arrange
            var trolley = new Trolley();
            _mockTrolleyRepository.Setup(r => r.GetTrolleyAsync()).ReturnsAsync(trolley);

            // Act
            var result = await _trolleyService.GetTrolleyAsync();

            // Assert
            Assert.Equal(trolley, result);
        }
    }
}