using Microsoft.EntityFrameworkCore;
using BeerAPI.Data;
using System.Globalization;

public class DatabaseSetup
{
    private readonly ApplicationDbContext _context;

    public DatabaseSetup(ApplicationDbContext context)
    {
        _context = context;
    }

    public void InitializeDatabase()
    {
        _context.Database.Migrate();
    }
}
