using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mataeem.DTOs.CategoryDTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> GetAllCategoriesByMenuId(Guid menuId)
        {
            var categories = await _context.Categories
                .Where(x => x.MenuId == menuId)
                .ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return categories;
        }

        public async Task<bool> CreateCategory(Guid menuId, CategorySaveDto model)
        {
            if (model == null || menuId == Guid.Empty) return false;

            var menu = await _context.Menus.FindAsync(menuId);

            if (menu == null) return false;

            var category = _mapper.Map<Category>(model);

            if (category == null) return false;

            category.CreateDate = DateTime.Now;
            category.IsActive = true;
            category.MenuId = menuId;

            _context.Categories.Add(category);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
        public async Task<bool> UpdateCategory(Guid id, CategorySaveDto model)
        {
            if (id == Guid.Empty) return false;

            var Category = await _context.Categories.FindAsync(id);

            if (Category == null) return false;

            _mapper.Map(model, Category);

            Category.LastUpdateDate = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty) return false;

            var category = await _context.Categories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return false;

            if (!category.Products!.Any())
            {
                _context.Categories.Remove(category);
            }
            else
            {
                category.LastUpdateDate = DateTime.Now;
                category.IsActive = false;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

    }
}
