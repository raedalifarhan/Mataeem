namespace Mataeem.DTOs
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; } = default!;
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public string? City { get; set; } = default!;
        public string? District { get; set; }
        public string? Address { get; set; } = default!;
        public string? ContactNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsOpen { get; set; }
        public string? Website { get; set; }

        public string? Menu { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
