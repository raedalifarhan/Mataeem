using Mataeem.Models;
using Mataeem.Models.OrderAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mataeem.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure the relationship between AppUser and Invoice
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Invoices) // AppUser has many Invoices
                .WithOne(i => i.Customer) // Invoice belongs to one AppUser
                .HasForeignKey(i => i.CustomerId); // Foreign key property in Invoice


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodBrand> FoodBrands { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<BusinessHours> BusinessHours { get; set; }
        
    }
}
