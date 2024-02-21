using System.ComponentModel.DataAnnotations;

namespace Mataeem.DTOs.CategoryDTOs
{
    public class CategorySaveDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
