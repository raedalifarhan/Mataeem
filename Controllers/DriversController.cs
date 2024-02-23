using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = $"{RolesNames.IT}")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;

        public DriversController(IDriverRepository DriverRepository)
        {
            _driverRepository = DriverRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDrivers()
        {
            //_driverRepository.AssignDriverToRestaurant()
            //_driverRepository.ReleaseDriverFromRestaurant();
            return Ok();
        }
    }
}