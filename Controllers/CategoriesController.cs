using Mataeem.DTOs.CategoryDTOs;
using Mataeem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{menuId}")]
        public async Task<ActionResult<List<CategoryListDto>>> GetAllCategoriesByMenuId(Guid menuId)
        {
            return Ok(await _categoryRepository.GetAllCategoriesByMenuId(menuId));
        }

        [HttpPost("{menuId}")]
        public async Task<ActionResult> CreateCategory(Guid menuId, CategorySaveDto model)
        {
            var result = await _categoryRepository.CreateCategory(menuId, model);

            if (!result) return BadRequest("An error accured during Save category, ");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(Guid id, CategorySaveDto model)
        {

            var result = await _categoryRepository.UpdateCategory(id, model);

            if (!result) return BadRequest("An error accured during Save Category ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {

            var result = await _categoryRepository.DeleteCategory(id);

            if (!result) return BadRequest("An error accured during dedctivate Category ");

            return Ok("deactivated Successfully!");
        }

    }
}