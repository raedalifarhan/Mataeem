using Mataeem.Models;

namespace Mataeem.DTOs.ProductDTOs
{
    public class OptionDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public ProductOptionType? Type { get; set; }
        public bool IsMandatory { get; set; } = false;
        public int MandatoryCount { get; set; }
        public List<OptionValueDto> Values { get; set; }
    }
}
