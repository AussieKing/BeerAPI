using BeerAPI.Models;
using BeerAPI.Services;

namespace BeerAPI.Tests.Services
{
    public class BeerDescriptionServiceTests
    {
        [Fact]
        public void GetDescription_ShouldContain_BeerName ()
        {
            // AAA method

            // Arrange
            var beer = new Beer { Name = "NotABeer" };

            // Act
            var sut = new BeerDescriptionService();
            var result = sut.GetDescription(beer);

            // Assert
            Assert.Contains("NotABeer", result);

        }
    }
}
