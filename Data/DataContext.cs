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
            // Configure the relationship between AppUser and Order
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.CustomerOrders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.DriverOrders)
                .WithOne(o => o.AssignedDriver)
                .HasForeignKey(o => o.DriverId)
                .IsRequired(false);

            // Ignore the DriverRestaurants property
            modelBuilder.Entity<AppUser>()
                .Ignore(u => u.DriverRestaurants);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .IsRequired(false); 

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.CreatedBy)
                .WithMany()
                .HasForeignKey(i => i.CreatedById)
                .IsRequired(); // Assuming CreatedBy is required

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.CreatedBy)
                .WithMany()
                .HasForeignKey(r => r.CreatedById)
                .IsRequired(); // Assuming CreatedBy is required

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Driver)
                .WithMany()
                .HasForeignKey(r => r.DriverId)
                .IsRequired(false); // Assuming DriverId can be null

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.UpdatedBy)
                .WithMany()
                .HasForeignKey(r => r.UpdatedById)
                .IsRequired(false); // Assuming UpdatedById can be null

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
