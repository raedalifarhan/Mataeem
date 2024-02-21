using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class BusinessHours : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; } = new();

        [Required]
        public TimeSpan OpenTime { get; set; }

        [Required]
        public TimeSpan CloseTime { get; set; }

        // nav properties
        public Restaurant? Restaurant { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}
