using BeerAPI.Controllers;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;

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
        public void GetBeerById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            _mockBeerService.Setup(service => service.GetBeerById(It.IsAny<int>())).Returns(default(Beer?));

            // Act
            var result = _controller.GetBeerById(999); // this ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);

            var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeActionResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public void UpdatePromoPrice_ValidRequest_UpdatesPromoPrice()
        {
            // Arrange
            var beerId = 1;
            var promoPriceUpdateRequest = new PromoPriceUpdateRequest { NewPromoPrice = 4.99M };
            var mockBeer = new Beer { Id = beerId, Name = "Lager", Price = 5.99M };

            _mockBeerService.Setup(service => service.GetBeerById(beerId)).Returns(mockBeer);
            _mockBeerService.Setup(service => service.UpdateBeer(It.IsAny<Beer>())).Callback<Beer>(beer =>
            {
                mockBeer.PromoPrice = beer.PromoPrice;
            });

            // Act
            var result = _controller.UpdatePromoPrice(beerId, promoPriceUpdateRequest);

            // Assert
            _mockBeerService.Verify(service => service.UpdateBeer(It.Is<Beer>(b => b.PromoPrice == promoPriceUpdateRequest.NewPromoPrice)), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
