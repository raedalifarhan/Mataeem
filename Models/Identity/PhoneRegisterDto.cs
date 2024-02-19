using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models.Identity
{
    public class PhoneRegisterDto
    {
        [Required]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        public string DisplayName { get; set; } = default!;
    }

}
