using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mataeem.DTOs.DriverDTOs;
using Mataeem.DTOs.DriverRestaurantDTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DriverRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AssignDriverToRestaurant(string driverId, Guid restaurantId)
        {
            if (!string.IsNullOrEmpty(driverId) && restaurantId != Guid.Empty)
            {
                _context.DriverRestaurants.Add(new DriverRestaurant
                {
                    DriverId = driverId,
                    RestaurantId = restaurantId,
                    AssignDate = DateTime.Now,
                    IsAssigned = true
                });

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return true;
            }

            return false;
        }

        public async Task<bool> ReleaseDriverFromRestaurant(Guid id)
        {
            var driverRestaurant = await _context.DriverRestaurants.FindAsync(id);

            driverRestaurant!.IsAssigned = false;

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return true;

            return false;
        }

        public async Task<List<DriverDto>> GetAllDriversByRestaurantId(Guid restaurantId, bool all = false)
        {
            var query = _context.DriverRestaurants
                .Include(x => x.Driver)
                .Where(x => x.RestaurantId == restaurantId)
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            if (!all) 
                query = query.Where(x => x.IsAssigned);

            return await query.ToListAsync();
        }

        public async Task<List<RestaurantDto>> GetAllRestaurantsByDriverId(string driverId, bool all = false)
        {
            var query = _context.DriverRestaurants
                .Include(x => x.Restaurant)
                .Where(x => x.DriverId == driverId)
                .ProjectTo<RestaurantDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            if (!all)
                query = query.Where(x => x.IsAssigned);

            return await query.ToListAsync();
        }
    }
}

