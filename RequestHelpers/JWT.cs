namespace Mataeem.Helpers
{
    public class JWT
    {
        public string Key { get; set; } = string.Empty;
        public string Issure { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInDays { get; set; }
    }
}
