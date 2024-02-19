using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string CategoryName { get; set; } = default!;

        // nav properties
        public Menu? Menu { get; set; }
        public Guid? MenuId { get; set; }

        public List<Product>? Products { get; set; }



        // auditing properties
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        
        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
    }
}