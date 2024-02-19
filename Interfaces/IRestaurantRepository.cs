using Mataeem.DTOs;

namespace Mataeem.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantDto>> GetAllRestaurants();

        Task<bool> CreateRestaurant(RestaurantSaveDto model);
        Task<bool> UpdateRestaurant(Guid id, RestaurantSaveDto model);
        Task<bool> DeleteRestaurant(Guid id);
    }
}
