using Mataeem.DTOs.RestaurantDTOs;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<RestaurantListDto>>> GetAllRestaurants([FromQuery] RestaurantParams param)
        {
            return Ok(await _restaurantRepository.GetAllRestaurants(param));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurant(Guid id)
        {
            return Ok(await _restaurantRepository.GetRestaurant(id));
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesNames.IT}")]
        public async Task<ActionResult> CreateRestaurant(RestaurantSaveDto model)
        {
            var result = await _restaurantRepository.CreateRestaurant(model);

            if (!result) return BadRequest("An error accured during Save restaurant, " +
                "check model field and picture extencion.");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesNames.IT}")]
        public async Task<ActionResult> UpdateRestaurant(Guid id, RestaurantSaveDto model)
        {

            var result = await _restaurantRepository.UpdateRestaurant(id, model);

            if (!result) return BadRequest("An error accured during Save restaurant ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesNames.IT}")]
        public async Task<ActionResult> DeleteRestaurant(Guid id)
        {

            var result = await _restaurantRepository.DeleteRestaurant(id);

            if (!result) return BadRequest("An error accured during dedctivate restaurant ");

            return Ok("deactivated Successfully!");
        }
        
        [HttpGet("assign/menu/{menuId}/restaurant/{restaurantId}")]
        [Authorize(Roles = $"{RolesNames.IT}, {RolesNames.SUPERADMIN}")]
        public async Task<ActionResult> AssignMenuToRestaurant(Guid menuId, Guid restaurantId)
        {
            var isSuccess = await _restaurantRepository.AssignMenuToRestaurant(menuId, restaurantId);

            if (!isSuccess) return BadRequest("An error occurec during assign menu to restaurant.");

            return Ok("Assigned successfuly");
        }

    }
}