namespace Mataeem.DTOs
{
    public class BussinessHoursDto
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}
