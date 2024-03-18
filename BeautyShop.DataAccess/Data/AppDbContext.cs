using BeautyShop.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyShop.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Makeup",
                DisplayOrder = 1
            },
            new Category
            {
                Id = 2,
                Name = "Hair",
                DisplayOrder = 2
            },
            new Category
            {
                Id = 3,
                Name = "Skincare",
                DisplayOrder = 3
            }
       );

    }
}