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
                .HasKey(t => new { t.BeerId });

            modelBuilder.Entity<TrolleyItem>()
                .HasOne(t => t.Beer)
                .WithMany()
                .HasForeignKey(t => t.BeerId);
        }
    }
}
