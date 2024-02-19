using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Product : BaseEntity
    {

        [Required]
        public string ProductName { get; set; } = default!;

        public string? PictureUrl { get; set; }

        public string? Description { get; set; }

        public float? Price { get; set; }

        public int? Rate { get; set; }


        // van porperties
        public Category? Category { get; set; }
        public Guid? CategoryId { get; set; }

        public FoodBrand? FoodBrand { get; set; }
        public Guid? FoodBrandId { get; set; }

        public Guid? ParentId { get; set; }

        public ICollection<Product>? Products { get; set; }


        // auditing properties
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
    }
}