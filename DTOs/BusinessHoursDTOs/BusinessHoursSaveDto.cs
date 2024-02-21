namespace Mataeem.DTOs.BusinessHoursDTOs
{
    public class BusinessHoursSaveDto
    {
        public DayOfWeek DayOfWeek { get; set; } = new();
        public List<HoursDto>? Values { get; set; }
    }
}