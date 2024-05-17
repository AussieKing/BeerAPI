using Microsoft.EntityFrameworkCore;
using BeerAPI.Models;

namespace BeerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<TrolleyItem> TrolleyItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrolleyItem>()
                .HasKey(t => new { t.BeerId });  // configuring the TrolleyItem entity to use BeerId as its primary key.

            modelBuilder.Entity<TrolleyItem>()
                .HasOne(t => t.Beer)
                .WithMany()
                .HasForeignKey(t => t.BeerId);  // configuring one-to-many relationship between TrolleyItem and Beer
        }
    }
}
