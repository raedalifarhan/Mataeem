using System.ComponentModel.DataAnnotations;

namespace Mataeem.DTOs.ProductDTOs
{
    public class ProductSaveDto
    {
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? RegularPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        
        public string? PictureBase64 { get; set; }
        public string? PictureExtension { get; set; }
        public List<OptionDto>? Options { get; set; }
    }
}
