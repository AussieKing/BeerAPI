using BeerAPI.Controllers;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace BeerAPI.Tests.Controllers
{
    public class BeersControllersTests
    {
        private readonly Mock<IBeerService> _mockBeerService;
        private readonly BeersController _controller;

        public BeersControllersTests()
        {
            _mockBeerService = new Mock<IBeerService>();
            _controller = new BeersController(_mockBeerService.Object); 
        }

        [Fact]
        public async Task GetBeerById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            _mockBeerService.Setup(service => service.GetBeerByIdAsync(It.IsAny<int>())).ReturnsAsync((Beer)null);

            // Act
            var result = await _controller.GetBeerById(999); // this ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);

            var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeActionResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task UpdatePromoPrice_ValidRequest_UpdatesPromoPrice()
        {
            // Arrange
            var beerId = 1;
            var promoPriceUpdateRequest = new PromoPriceUpdateRequest { NewPromoPrice = 4.99M };
            var mockBeer = new Beer { Id = beerId, Name = "Lager", Price = 5.99M };

            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId)).ReturnsAsync(mockBeer);
            _mockBeerService.Setup(service => service.UpdatePromoPriceAsync(It.IsAny<int>(), It.IsAny<decimal?>()))
                .Callback<int, decimal?>((id, newPromoPrice) =>
                {
                    mockBeer.PromoPrice = newPromoPrice;
                }).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePromoPrice(beerId, promoPriceUpdateRequest);

            // Assert
            _mockBeerService.Verify(service => service.UpdatePromoPriceAsync(beerId, promoPriceUpdateRequest.NewPromoPrice), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBeer_ValidRequest_UpdatesBeer()
        {
            // Arrange
            var beerId = 1;
            var updateBeerRequest = new UpdateBeerRequest { Name = "Updated Lager", Price = 6.99M, PromoPrice = 5.49M };
            var mockBeer = new Beer { Id = beerId, Name = "Lager", Price = 5.99M };

            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId)).ReturnsAsync(mockBeer);
            _mockBeerService.Setup(service => service.UpdateBeerAsync(It.IsAny<int>(), It.IsAny<UpdateBeerRequest>()))
                .Callback<int, UpdateBeerRequest>((id, updateRequest) =>
                {
                    mockBeer.Name = updateRequest.Name;
                    mockBeer.Price = updateRequest.Price;
                    mockBeer.PromoPrice = updateRequest.PromoPrice;
                }).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateBeer(beerId, updateBeerRequest);

            // Assert
            _mockBeerService.Verify(service => service.UpdateBeerAsync(beerId, updateBeerRequest), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
