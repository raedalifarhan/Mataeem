using Mataeem.DTOs.DriverDTOs;
using Mataeem.DTOs.DriverRestaurantDTOs;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;

        public DriversController(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        [Authorize(Roles = $"{RolesNames.IT}, {RolesNames.SUPERADMIN}")]
        [HttpGet("assign/driver/{driverId}/restaurant/{restaurantId}")]
        public async Task<ActionResult> AssignDriverToRestaurant(string driverId, Guid restaurantId)
        {
            var isSuccess = await _driverRepository.AssignDriverToRestaurant(driverId, restaurantId);

            if (!isSuccess)
                return BadRequest("An error occurred during assignment");

            return Ok("Assignment Successful");
        }

        [HttpGet("release/{id}")]
        [Authorize(Roles = $"{RolesNames.IT}, {RolesNames.SUPERADMIN}")]
        public async Task<ActionResult> ReleaseDriverFromRestaurant(Guid id)
        {
            var isSuccess = await _driverRepository.ReleaseDriverFromRestaurant(id);

            if (!isSuccess)
                return BadRequest("An error occurred during releasing driver from restaurant");

            return Ok("Release successful");
        }

        [HttpGet("all-drivers/{restaurantId}")]
        public async Task<ActionResult<List<DriverDto>>> GetAllDriversByRestaurantId(Guid restaurantId,
            [FromQuery] bool all = false)
        {
            return Ok(await _driverRepository.GetAllDriversByRestaurantId(restaurantId, all));
        }

        [HttpGet("all-restaurants/{driverId}")]
        public async Task<ActionResult<List<RestaurantDto>>> GetAllRestaurantsByDriverId(string driverId,
            [FromQuery] bool all = false)
        {
            return Ok(await _driverRepository.GetAllRestaurantsByDriverId(driverId, all));
        }
    }

}