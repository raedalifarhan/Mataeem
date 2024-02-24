
using Mataeem.DTOs.CategoryDTOs;

namespace Mataeem.DTOs.MenuDTOs
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public ICollection<CategoryDto>? Categories { get; set; }
    }
}
