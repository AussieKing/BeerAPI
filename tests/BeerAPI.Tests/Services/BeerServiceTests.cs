using BeerAPI.Models;
using BeerAPI.Services;
using Xunit;
using Microsoft.Data.Sqlite;
using Dapper;

namespace BeerAPI.Tests.Services
{
    public class BeerServiceTests : IDisposable
    {
        private readonly BeerService _service;
        private readonly SqliteConnection _connection;

        public BeerServiceTests()
        {
            // Settingup an in-memory SQLite database
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            SetupDatabase(_connection);

            _service = new BeerService(_connection.ConnectionString);
        }

        private void SetupDatabase(SqliteConnection connection)
        {
            // Create the necessary tables for testing
            connection.Execute("CREATE TABLE Beers (Id INTEGER PRIMARY KEY, Name TEXT, Price REAL, PromoPrice REAL);");
            // Seed data if necessary
            connection.Execute("INSERT INTO Beers (Name, Price) VALUES ('Lager', 5.99), ('IPA', 10.00);");
        }

        [Fact]
        public void AddBeer_ShouldBeInList()
        {
            // Arrange
            var newBeer = new Beer { Name = "Test Beer", Price = 5.99M };

            // Act
            var addedBeer = _service.AddBeer(newBeer);

            // Assert
            Assert.Contains(addedBeer, _service.GetAllBeers());
            Assert.Equal("Test Beer", addedBeer.Name);
            // Ensure that an ID has been assigned
            Assert.True(addedBeer.Id > 0);
        }

        [Fact]
        public void GetBeerById_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = 999; // ID that does not exist in the initial list

            // Act
            var beer = _service.GetBeerById(nonExistingId);

            // Assert
            Assert.Null(beer);
        }

        [Fact]
        public void DeleteBeer_ExistingId_ShouldRemoveFromList()
        {
            // Arrange
            var beerIdToDelete = 1; // this ID exists in my initial list

            // Act
            var deleteResult = _service.DeleteBeer(beerIdToDelete);
            var beerAfterDelete = _service.GetBeerById(beerIdToDelete);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(beerAfterDelete);
        }

        [Fact]
        public void UpdateBeer_ExistingId_UpdatesBeer()
        {
            // Arrange
            var beerToUpdate = new Beer { Id = 1, Name = "Updated Lager", Price = 6.99M, PromoPrice = 4.99M };
            _service.AddBeer(new Beer { Id = 1, Name = "Lager", Price = 5.99M }); 

            // Act
            _service.UpdateBeer(beerToUpdate);

            // Assert
            var updatedBeer = _service.GetBeerById(1);
            Assert.NotNull(updatedBeer);
            Assert.Equal("Updated Lager", updatedBeer?.Name);
            Assert.Equal(6.99M, updatedBeer?.Price);
            Assert.Equal(4.99M, updatedBeer?.PromoPrice);
        }
        public void Dispose()
        {
            // Cleanup and close the in-memory database opened earlier
            _connection.Close();
        }
    }
}
