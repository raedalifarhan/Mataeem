using Mataeem.DTOs.ProductDTOs;

namespace Mataeem.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = default!;
        public ICollection<ProductDto>? Products { get; set; }
    }
}
