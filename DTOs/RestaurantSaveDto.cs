using System.ComponentModel.DataAnnotations;

namespace Mataeem.DTOs
{
    public class RestaurantSaveDto
    {
        [Required]
        public string RestaurantName { get; set; } = default!;

        public double? LocationLatitude { get; set; }

        public double? LocationLongitude { get; set; }

        public string? City { get; set; } = default!;

        public string? District { get; set; }

        public string? Address { get; set; } = default!;

        public string? ContactNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }
    }
}
