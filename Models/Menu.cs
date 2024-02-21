using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Menu : BaseEntity
    {

        [Required]
        public string MenuName { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // nav porperties
        public ICollection<Category>? Categories { get; set; }
        public ICollection<FoodBrand>? FoodBrands { get; set; }
        public ICollection<Restaurant>? Restaurants { get; set; }



        // auditing properties
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
    }
}