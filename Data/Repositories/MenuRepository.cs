using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mataeem.DTOs.MenuDTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MenuRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuListDto>> GetAllMenus()
        {
            var menus = await _context.Menus
                .ProjectTo<MenuListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return menus;
        }

        public async Task<bool> CreateMenu(MenuSaveDto model)
        {
            if (model == null) return false;

            var menu = _mapper.Map<Menu>(model);

            if (menu == null) return false;

            menu.CreateDate = DateTime.Now;
            menu.IsActive = true;

            _context.Menus.Add(menu);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
        public async Task<bool> UpdateMenu(Guid id, MenuSaveDto model)
        {
            if (id == Guid.Empty) return false;

            var Menu = await _context.Menus.FindAsync(id);

            if (Menu == null) return false;

            _mapper.Map(model, Menu);

            Menu.LastUpdateDate = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> DeleteMenu(Guid id)
        {
            if (id == Guid.Empty) return false;

            var menu = await _context.Menus
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (menu == null) return false;

            if (!menu.Categories!.Any())
            {
                _context.Menus.Remove(menu);
            }
            else
            {
                menu.LastUpdateDate = DateTime.Now;
                menu.IsActive = false;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

    }
}
