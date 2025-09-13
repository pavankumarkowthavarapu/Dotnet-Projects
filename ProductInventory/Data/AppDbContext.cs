using Microsoft.EntityFrameworkCore;
using ProductInventory.Models;

namespace ProductInventory.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 75000.00m,
                    Quantity = 10,
                    Category = "Electronics"
                },
        new Product
        {
            Id = 2,
            Name = "Office Chair",
            Price = 5000.00m,
            Quantity = 25,
            Category = "Furniture"
        },
        new Product
        {
            Id = 3,
            Name = "Notebook",
            Price = 50.00m,
            Quantity = 200,
            Category = "Stationery"
        }
            );
        }
    }
}
