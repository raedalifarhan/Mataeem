using AutoMapper;
using Mataeem.DTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class BussinessHoursRepository : IBussinessHoursRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BussinessHoursRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RestaurantDto>> GetBussinessHoursByRestaurnatId(Guid restaurantId)
        {
            var query = await _context.BusinessHours
                .Where(x => x.RestaurantId == restaurantId)
                .ToListAsync();

            return _mapper.Map<List<RestaurantDto>>(query);
        }

        public async Task<bool> CreateRestaurant(RestaurantSaveDto model)
        {
            if (model == null) return false;

            var restaurant = _mapper.Map<Restaurant>(model);

            restaurant.CreateDate = DateTime.Now;
            
            _context.Restaurants.Add(restaurant);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> UpdateRestaurant(Guid id, RestaurantSaveDto model)
        {
            if (id == Guid.Empty) return false;

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null) return false;

            _mapper.Map(model, restaurant);

            restaurant.LastUpdateDate = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> DeleteRestaurant(Guid id)
        {
            if (id == Guid.Empty) return false;

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null) return false;

            restaurant.LastUpdateDate = DateTime.Now;
            restaurant.IsActive = false;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
    }
}
