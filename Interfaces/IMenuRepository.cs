using Mataeem.DTOs.MenuDTOs;

namespace Mataeem.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuListDto>> GetAllMenus();

        Task<bool> CreateMenu(MenuSaveDto model);
        Task<bool> UpdateMenu(Guid id, MenuSaveDto model);
        Task<bool> DeleteMenu(Guid id);
    }
}
