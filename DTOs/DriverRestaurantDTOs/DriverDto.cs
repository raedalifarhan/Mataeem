namespace Mataeem.DTOs.DriverDTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; }

        public Guid AssignId { get; set; }
        public string DisplayName { get; set; }
        public string? PhoneNumber { get; set; }

        public bool IsAssigned { get; set; }
    }
}
