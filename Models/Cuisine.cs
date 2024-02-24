using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Cuisine : BaseEntity
    {
        [Required]
        public string CuisineName { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        // nav properties
        public List<Product>? Products { get; set; }


        // auditing properties
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
        
        public ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }
    }
}