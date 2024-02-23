using Mataeem.DTOs.MenuDTOs;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class MenusController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenusController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuListDto>>> GetAllMenus()
        {
            return Ok(await _menuRepository.GetAllMenus());
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> CreateMenu(MenuSaveDto model)
        {
            var result = await _menuRepository.CreateMenu(model);

            if (!result) return BadRequest("An error accured during Save menu, ");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> UpdateMenu(Guid id, MenuSaveDto model)
        {

            var result = await _menuRepository.UpdateMenu(id, model);

            if (!result) return BadRequest("An error accured during Save Menu ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> DeleteMenu(Guid id)
        {

            var result = await _menuRepository.DeleteMenu(id);

            if (!result) return BadRequest("An error accured during dedctivate Menu ");

            return Ok("deactivated Successfully!");
        }

    }
}