using Mataeem.DTOs.BusinessHoursDTOs;
using Mataeem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessHoursController : ControllerBase
    {
        private readonly IBusinessHoursRepository _businessHoursRepository;

        public BusinessHoursController(IBusinessHoursRepository businessHoursRepository)
        {
            _businessHoursRepository = businessHoursRepository;
        }

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<List<BusinessHoursSaveDto>>> GetAll(Guid restaurantId)
        {
            return await _businessHoursRepository
                .GetBusinessHoursByRestaurnatId(restaurantId);
        }

        [HttpPut("{restaurantId}")]
        public async Task<ActionResult> UpdateBusinessHours(Guid restaurantId, [FromBody] List<BusinessHoursSaveDto> model)
        {

            var result = await _businessHoursRepository.UpdateBusinessHours(restaurantId, model);

            if (!result) return BadRequest("An error accured during Save restaurant ");

            return Ok("Updated Successfully");
        }
    }
}