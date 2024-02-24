using Mataeem.DTOs.DriverDTOs;
using Mataeem.DTOs.DriverRestaurantDTOs;

namespace Mataeem.Interfaces
{
    public interface IDriverRepository
    {
        Task<bool> AssignDriverToRestaurant(string driverId, Guid restaurantId);
        Task<bool> ReleaseDriverFromRestaurant(Guid id);

        Task<List<DriverDto>> GetAllDriversByRestaurantId(Guid restaurantId, bool all = false);

        Task<List<RestaurantDto>> GetAllRestaurantsByDriverId(string driverId, bool all = false);
    }
}
