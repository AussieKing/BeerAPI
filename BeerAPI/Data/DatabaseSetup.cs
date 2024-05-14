using Microsoft.EntityFrameworkCore;
using BeerAPI.Data;

public class DatabaseSetup
{
    private readonly ApplicationDbContext _context;

    public DatabaseSetup(ApplicationDbContext context)
    {
        _context = context;
    }

    public void InitializeDatabase()
    {
        _context.Database.EnsureCreated();
    }
}
