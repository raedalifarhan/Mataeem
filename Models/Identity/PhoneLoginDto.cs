using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models.Identity
{
    public class PhoneLoginDto
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string VerificationCode { get; set; } = string.Empty;
    }
}
