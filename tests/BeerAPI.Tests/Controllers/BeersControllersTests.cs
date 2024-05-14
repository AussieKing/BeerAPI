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
        private readonly Mock<IBeerDescriptionService> _mockBeerDescriptionService;
        private readonly BeersController _controller;

        public BeersControllersTests()
        {
            _mockBeerService = new Mock<IBeerService>();
            _mockBeerDescriptionService = new Mock<IBeerDescriptionService>();
            _controller = new BeersController(_mockBeerService.Object, _mockBeerDescriptionService.Object);
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
            _mockBeerService.Setup(service => service.UpdateBeerAsync(It.IsAny<Beer>())).Callback<Beer>(beer =>
            {
                mockBeer.PromoPrice = beer.PromoPrice;
            }).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePromoPrice(beerId, promoPriceUpdateRequest);

            // Assert
            _mockBeerService.Verify(service => service.UpdateBeerAsync(It.Is<Beer>(b => b.PromoPrice == promoPriceUpdateRequest.NewPromoPrice)), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
