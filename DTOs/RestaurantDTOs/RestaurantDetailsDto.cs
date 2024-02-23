using Mataeem.DTOs.MenuDTOs;

namespace Mataeem.DTOs.RestaurantDTOs
{
    public class RestaurantDetailsDto
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; } = default!;
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public string? City { get; set; } = default!;
        public string? District { get; set; }
        public string? Address { get; set; } = default!;
        public string? ContactNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Rate { get; set; }

        public bool IsOpen { get; set; }
        public bool IsActive { get; set; }
        public string? Website { get; set; }
        public string? PictureUrl { get; set; }
        public MenuDto? Menu { get; set; }
    }
}
