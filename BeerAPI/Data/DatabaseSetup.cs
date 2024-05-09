// Database setup
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

public class DatabaseSetup
{
    private readonly string _connectionString;

    public DatabaseSetup(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString); // using to keep connection closed and dispose of it 
        connection.Open();

        var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Beers'; ");
        string? tableName = table.FirstOrDefault();
        if (string.IsNullOrEmpty(tableName)) 
        {
            connection.Execute("CREATE TABLE Beers (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price REAL);"); 

        }
    }

}