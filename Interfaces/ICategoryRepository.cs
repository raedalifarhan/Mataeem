using Mataeem.DTOs.CategoryDTOs;

namespace Mataeem.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryListDto>> GetAllCategoriesByMenuId(Guid menuId);

        Task<bool> CreateCategory(Guid menuId, CategorySaveDto model);
        Task<bool> UpdateCategory(Guid id, CategorySaveDto model);
        Task<bool> DeleteCategory(Guid id);
    }
}
