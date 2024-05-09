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
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Ensure that the Beers table exist
        EnsureTableExists(connection, "Beers", "CREATE TABLE Beers (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price REAL);");

        // Ensure that the TrolleyItems table exists
        EnsureTableExists(connection, "TrolleyItems", "CREATE TABLE TrolleyItems (BeerId INTEGER, Quantity INTEGER DEFAULT 1, PRIMARY KEY(BeerId), FOREIGN KEY(BeerId) REFERENCES Beers(Id));");
    }

    private void EnsureTableExists(IDbConnection connection, string tableName, string createTableSql)
    {
        var table = connection.Query<string>($"SELECT name FROM sqlite_master WHERE type='table' AND name = '{tableName}';");
        if (!table.Any())
        {
            connection.Execute(createTableSql);
        }
    }
}
