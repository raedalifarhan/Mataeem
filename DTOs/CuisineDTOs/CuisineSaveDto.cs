using System.ComponentModel.DataAnnotations;

namespace Mataeem.DTOs.CuisineDTOs
{
    public class CuisineSaveDto
    {
        [Required]
        public string CuisineName { get; set; }
    }
}
