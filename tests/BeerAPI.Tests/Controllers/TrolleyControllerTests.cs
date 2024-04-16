using BeerAPI.Controllers;
using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BeerAPI.Tests.Controllers
{
    public class TrolleyControllerTests
    {
        [Fact]
        public void AddItemToTrolley_ShouldAddItemForIndividualUsers()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddScoped<ITrolleyService, TrolleyService>();

            // Creating service providers for two different scopes (simulating two users)
            var serviceProviderUser1 = services.BuildServiceProvider().CreateScope().ServiceProvider;
            var serviceProviderUser2 = services.BuildServiceProvider().CreateScope().ServiceProvider;

            var trolleyServiceUser1 = serviceProviderUser1.GetService<ITrolleyService>();
            var trolleyServiceUser2 = serviceProviderUser2.GetService<ITrolleyService>();

            Assert.NotNull(trolleyServiceUser1); // Assert that the service is not null
            Assert.NotNull(trolleyServiceUser2); // Assert that the service is not null

            // Act
            trolleyServiceUser1.AddItem(new Beer { Id = 1, Name = "Test Beer 1", Price = 5.99M });
            trolleyServiceUser2.AddItem(new Beer { Id = 2, Name = "Test Beer 2", Price = 6.99M });

            // Assert
            Assert.Single(trolleyServiceUser1.GetTrolley().Items);
            Assert.Single(trolleyServiceUser2.GetTrolley().Items);
        }

        [Fact]
        public void RemoveItemFromTrolley_ShouldReflectIndividualUserChanges()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddScoped<ITrolleyService, TrolleyService>();
            var serviceProvider = services.BuildServiceProvider();

            using var scopeUser1 = serviceProvider.CreateScope();
            var trolleyServiceUser1 = scopeUser1.ServiceProvider.GetService<ITrolleyService>();
            Assert.NotNull(trolleyServiceUser1); // Ensure the service is not null
            trolleyServiceUser1.AddItem(new Beer { Id = 1, Name = "Test Beer", Price = 5.99M });

            using var scopeUser2 = serviceProvider.CreateScope();
            var trolleyServiceUser2 = scopeUser2.ServiceProvider.GetService<ITrolleyService>();
            Assert.NotNull(trolleyServiceUser2); // Ensure the service is not null
            trolleyServiceUser2.AddItem(new Beer { Id = 2, Name = "Test Beer", Price = 6.99M });

            // Act
            var removeResultUser1 = trolleyServiceUser1.RemoveItem(1);

            // Assert
            Assert.True(removeResultUser1);
            Assert.Empty(trolleyServiceUser1.GetTrolley().Items);
            Assert.Single(trolleyServiceUser2.GetTrolley().Items);
        }
    }
}
