using Mataeem.DTOs.RestaurantDTOs;
using Mataeem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [AllowAnonymous]
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
        public async Task<ActionResult<List<RestaurantListDto>>> GetAllRestaurants()
        {
            return Ok(await _restaurantRepository.GetAllRestaurants());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurant(Guid id)
        {
            return Ok(await _restaurantRepository.GetRestaurant(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant(RestaurantSaveDto model)
        {
            var result = await _restaurantRepository.CreateRestaurant(model);

            if (!result) return BadRequest("An error accured during Save restaurant, " +
                "check model field and picture extencion.");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(Guid id, RestaurantSaveDto model)
        {

            var result = await _restaurantRepository.UpdateRestaurant(id, model);

            if (!result) return BadRequest("An error accured during Save restaurant ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(Guid id)
        {

            var result = await _restaurantRepository.DeleteRestaurant(id);

            if (!result) return BadRequest("An error accured during dedctivate restaurant ");

            return Ok("deactivated Successfully!");
        }

    }
}