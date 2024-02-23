using Mataeem.Models.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string DisplayName { get; set; } = default!;
        public string? VerificationCode { get; set; }
        public bool IsAvailableDriver { get; set; }


        // Other relevant customer properties
        public ICollection<Order>? DriverOrders { get; set; }
        public ICollection<Order>? CustomerOrders { get; set; }

        public ICollection<CustomerBasket>? CustomerBaskets { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }

        public ICollection<DriverRestaurant>? DriverRestaurants { get; set; }


        // audeting props
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
