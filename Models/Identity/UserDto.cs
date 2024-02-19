namespace Mataeem.Models.Identity
{
    // Contain properties return back after loging or register.
    public class UserDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}