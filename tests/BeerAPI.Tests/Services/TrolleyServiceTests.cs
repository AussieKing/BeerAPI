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
        private TrolleyService _trolleyService;
        private Mock<ITrolleyRepository> _mockTrolleyRepository;

        public TrolleyServiceTests()
        {
            _mockTrolleyRepository = new Mock<ITrolleyRepository>();
            _trolleyService = new TrolleyService(_mockTrolleyRepository.Object);
        }

        [Fact]
        public void AddItem_ShouldAddToTrolley()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "Test Beer", Price = 5.99M };
            _mockTrolleyRepository.Setup(r => r.GetTrolley()).Returns(new Trolley());

            // Act
            _trolleyService.AddItem(beer);

            // Assert
            _mockTrolleyRepository.Verify(r => r.AddBeerToTrolley(It.IsAny<Beer>()), Times.Once);
        }

        [Fact]
        public void RemoveItem_ShouldDecreaseQuantity()
        {
            // Arrange
            var beerId = 1;
            _mockTrolleyRepository.Setup(r => r.RemoveBeerFromTrolley(beerId)).Returns(true);

            // Act
            var result = _trolleyService.RemoveItem(beerId);

            // Assert
            Assert.True(result);
            _mockTrolleyRepository.Verify(r => r.RemoveBeerFromTrolley(beerId), Times.Once);
        }

        [Fact]
        public void GetTrolley_ReturnsTrolley()
        {
            // Arrange
            var trolley = new Trolley();
            _mockTrolleyRepository.Setup(r => r.GetTrolley()).Returns(trolley);

            // Act
            var result = _trolleyService.GetTrolley();

            // Assert
            Assert.Equal(trolley, result);
        }
    }
}
