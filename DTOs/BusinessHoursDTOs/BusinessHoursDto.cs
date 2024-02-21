namespace Mataeem.DTOs.BusinessHoursDTOs
{
    public class BusinessHoursDto
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}
