namespace Mataeem.DTOs.ProductDTOs
{
    public class OptionValueDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal? RegularPrice { get; set; }
        public decimal? SellingPrice { get; set; }
    }
}
