using ElectronicsShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>()
            .HasData(new List<Category>
            {
                new() { Id = 1, CategoryName = "TVs" },
                new() { Id = 2, CategoryName = "Laptops" },
                new() { Id = 3, CategoryName = "Sound Systems" }
            });

        base.OnModelCreating(builder);
    }
}