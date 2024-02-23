namespace Mataeem.Models
{
    public class DriverRestaurant : BaseEntity
    {
        public string DriverId { get; set; }
        public AppUser Driver { get; set; }

        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public bool IsAssigned { get; set; }

        public DateTime AssignDate { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
