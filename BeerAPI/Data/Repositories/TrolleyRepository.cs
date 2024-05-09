using BeerAPI.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace BeerAPI.Repositories
{
    public class TrolleyRepository : ITrolleyRepository
    {
        private readonly IDbConnection _db;

        public TrolleyRepository(string connectionString)
        {
            _db = new SqliteConnection(connectionString);
        }

        public void AddBeerToTrolley(Beer beer)
        {
            var sql = @"INSERT INTO TrolleyItems (BeerId, Quantity) VALUES (@BeerId, 1)
                        ON CONFLICT(BeerId) DO UPDATE SET Quantity = Quantity + 1;";
            _db.Execute(sql, new { BeerId = beer.Id });
        }

        public bool RemoveBeerFromTrolley(int beerId)
        {
            var sql = @"DELETE FROM TrolleyItems WHERE BeerId = @BeerId AND Quantity > 1;
                        UPDATE TrolleyItems SET Quantity = Quantity - 1 WHERE BeerId = @BeerId;";
            return _db.Execute(sql, new { BeerId = beerId }) > 0;
        }

        public Trolley GetTrolley()
        {
            // SQL to fetch trolley items with associated beer data
            var sql = @"SELECT ti.Quantity, b.Id as BeerId, b.Name, b.Price
                FROM TrolleyItems ti 
                JOIN Beers b ON ti.BeerId = b.Id;";

            var trolleyItems = _db.Query<TrolleyItem, Beer, TrolleyItem>(
                sql,
                (trolleyItem, beer) => {
                    trolleyItem.Beer = beer;
                    return trolleyItem;
                },
                splitOn: "BeerId"  
            ).ToList();

            return new Trolley { Items = trolleyItems };
        }

    }
}
