namespace Mataeem.DTOs.RestaurantDTOs
{
    public class RestaurantListDto
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; } = default!;

        public double Distance { get; set; }
        public string? City { get; set; } = default!;
        public string? District { get; set; }
        public string? Address { get; set; } = default!;
        public bool IsOpen { get; set; }

        public int? Rate { get; set; }

        public string? PictureUrl { get; set; }
    }
}
