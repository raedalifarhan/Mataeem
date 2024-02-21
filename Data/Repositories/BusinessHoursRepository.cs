using AutoMapper;
using Mataeem.DTOs.BusinessHoursDTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class BusinessHoursRepository : IBusinessHoursRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BusinessHoursRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BusinessHoursSaveDto>> GetBusinessHoursByRestaurnatId(Guid restaurantId)
        {
            var query = await _context.BusinessHours
            .Where(x => x.RestaurantId == restaurantId)
            .ToListAsync();

            var businessHoursSaveDtos = query
                .GroupBy(x => x.DayOfWeek)
                .Select(group => new BusinessHoursSaveDto
                {
                    DayOfWeek = group.Key,
                    Values = group.Select(x => new HoursDto
                    {
                        OpenTime = x.OpenTime,
                        CloseTime = x.CloseTime,
                    }).ToList()
                }).ToList();



            return businessHoursSaveDtos;
        }

        public async Task<bool> UpdateBusinessHours(Guid restaurantId, List<BusinessHoursSaveDto> model)
        {
            if (restaurantId == Guid.Empty || model == null) return false;

            var BusinessHours = await _context.BusinessHours
                .Where(x => x.RestaurantId == restaurantId)
                .ExecuteDeleteAsync();

            model.ForEach(days =>
            {
                if (days.Values != null){
                    days.Values?.ForEach(hours =>
                    {
                        _context.BusinessHours.Add(
                            new BusinessHours
                            {
                                RestaurantId = restaurantId,
                                DayOfWeek = days.DayOfWeek,
                                OpenTime = hours.OpenTime,
                                CloseTime = hours.CloseTime,
                            });
                    });
                }
            });

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
    }
}
