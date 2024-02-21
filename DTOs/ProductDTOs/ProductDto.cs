namespace Mataeem.DTOs.ProductDTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public int RegularPrice { get; set; }
        public int SellingPrice { get; set; }
        public int? Rate { get; set; }
        public string PictureUrl { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}
