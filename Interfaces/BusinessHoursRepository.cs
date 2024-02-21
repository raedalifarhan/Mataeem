using Mataeem.DTOs.BusinessHoursDTOs;

namespace Mataeem.Interfaces
{
    public interface IBusinessHoursRepository
    {
        Task<List<BusinessHoursSaveDto>> GetBusinessHoursByRestaurnatId(Guid restaurantId);
        
        Task<bool> UpdateBusinessHours(Guid restaurantId, List<BusinessHoursSaveDto> model);
    }
}
