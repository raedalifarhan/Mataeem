using Mataeem.DTOs.ProductDTOs;

namespace Mataeem.DTOs.CuisineDTOs
{
    public class CuisineDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = default!;
        public ICollection<ProductDto>? Products { get; set; }
    }
}
