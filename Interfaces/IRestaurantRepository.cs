using Mataeem.DTOs.RestaurantDTOs;
using Mataeem.RequestHelpers;

namespace Mataeem.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<PagedList<RestaurantListDto>> GetAllRestaurants(RestaurantParams param);
        Task<RestaurantDetailsDto> GetRestaurant(Guid id);
        Task<bool> CreateRestaurant(RestaurantSaveDto model);
        Task<bool> UpdateRestaurant(Guid id, RestaurantSaveDto model);
        Task<bool> DeleteRestaurant(Guid id);
        Task<bool> AssignMenuToRestaurant(Guid menuId, Guid restaurantId);
    }
}
