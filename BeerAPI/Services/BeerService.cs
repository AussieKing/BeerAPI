using BeerAPI.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace BeerAPI.Services
{
    public class BeerService : IBeerService
    {
        private readonly IDbConnection _db;

        public BeerService(string connectionString)
        {
            _db = new SqliteConnection(connectionString);
        }

        public Beer GetBeerById(int beerId)
        {
            return _db.Query<Beer>("SELECT * FROM Beers WHERE Id = @Id", new { Id = beerId }).FirstOrDefault();
        }

        public List<Beer> GetAllBeers()
        {
            return _db.Query<Beer>("SELECT * FROM Beers").ToList();
        }

        public Beer AddBeer(Beer newBeer)
        {
            var sql = @"INSERT INTO Beers (Name, Price) VALUES (@Name, @Price);
                        SELECT CAST(last_insert_rowid() AS int);";
            newBeer.Id = _db.Query<int>(sql, newBeer).Single();
            return newBeer;
        }

        public bool DeleteBeer(int id)
        {
            return _db.Execute("DELETE FROM Beers WHERE Id = @Id", new { Id = id }) > 0;
        }

        public void UpdateBeer(Beer updatedBeer)
        {
            var sql = @"UPDATE Beers SET Name = @Name, Price = @Price, PromoPrice = @PromoPrice WHERE Id = @Id";
            _db.Execute(sql, updatedBeer);
        }
    }
}
