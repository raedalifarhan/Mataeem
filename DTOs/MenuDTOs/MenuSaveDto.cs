using System.ComponentModel.DataAnnotations;

namespace Mataeem.DTOs.MenuDTOs
{
    public class MenuSaveDto
    {
        [Required]
        public string MenuName { get; set; }
    }
}
