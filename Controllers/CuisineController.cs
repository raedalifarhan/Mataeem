using Mataeem.DTOs.CuisineDTOs;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineRepository _cuisineRepository;

        public CuisineController(ICuisineRepository cuisineRepository)
        {
            _cuisineRepository = cuisineRepository;
        }

        [HttpPost("{restaurantId}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> CreateCuisine(Guid restaurantId, CuisineSaveDto model)
        {
            var result = await _cuisineRepository.CreateCuisine(model);

            if (!result) return BadRequest("An error accured during Save Cuisine, ");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> UpdateCuisine(Guid id, CuisineSaveDto model)
        {

            var result = await _cuisineRepository.UpdateCuisine(id, model);

            if (!result) return BadRequest("An error accured during Save Cuisine ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> DeleteCuisine(Guid id)
        {

            var result = await _cuisineRepository.DeleteCuisine(id);

            if (!result) return BadRequest("An error accured during dedctivate Cuisine ");

            return Ok("deactivated Successfully!");
        }
        
        [Authorize(Roles =$"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        [HttpPost("relate/restaurant/{restaurantId}/cuisine/{cuisineId}")]
        public async Task<IActionResult> RelateRestauratnWithCuisine(Guid restaurantId, Guid cuisineId)
        {
            var success = await _cuisineRepository
                .RelateRestauratnWithCuisine(restaurantId, cuisineId);
            if (success)
                return Ok("Related successfull");
            else
                return NotFound("Invalid restaurant or cuisine ID, or relationship already exists.");
        }

        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        [HttpPost("release/restaurant/{restaurantId}/cuisine/{cuisineId}")]
        public async Task<IActionResult> ReleaseRestaurantAndCuisine(Guid restaurantId, Guid cuisineId)
        {
            var success = await _cuisineRepository
                .ReleaseRelationBetweenRestaurantAndCuisine(restaurantId, cuisineId);

            if (success)
                return Ok("Released successfull");
            else
                return NotFound("Relationship does not exist.");
        }

    }
}