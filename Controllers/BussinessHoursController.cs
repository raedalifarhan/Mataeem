using Mataeem.DTOs;
using Mataeem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class BussinessHoursController : ControllerBase
    {
        private readonly IBussinessHoursRepository _bussinessHoursRepository;

        public BussinessHoursController(IBussinessHoursRepository bussinessHoursRepository)
        {
            _bussinessHoursRepository = bussinessHoursRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestaurantDto>>> GetAll()
        {
            return Ok(await _bussinessHoursRepository.GetAllRestaurants());
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant(RestaurantSaveDto model)
        {
            var result = await _bussinessHoursRepository.CreateRestaurant(model);

            if (!result) return BadRequest("An error accured during Save restaurant ");

            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(Guid id, RestaurantSaveDto model)
        {

            var result = await _bussinessHoursRepository.UpdateRestaurant(id, model);

            if (!result) return BadRequest("An error accured during Save restaurant ");

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(Guid id)
        {

            var result = await _bussinessHoursRepository.DeleteRestaurant(id);

            if (!result) return BadRequest("An error accured during dedctivate restaurant ");

            return Ok("deactivated Successfully!");
        }

    }
}