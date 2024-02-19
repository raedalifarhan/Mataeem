namespace Mataeem.Models.Identity
{
    public class UserIdRolesDto
    {
        public string UserId { get; set; } = default!;
        public IList<string>? Roles { get; set; }
    }
}
