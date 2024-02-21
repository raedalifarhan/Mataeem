using Mataeem.DTOs.RestaurantDTOs;

namespace Mataeem.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantListDto>> GetAllRestaurants();
        Task<RestaurantDetailsDto> GetRestaurant(Guid id);
        Task<bool> CreateRestaurant(RestaurantSaveDto model);
        Task<bool> UpdateRestaurant(Guid id, RestaurantSaveDto model);
        Task<bool> DeleteRestaurant(Guid id);
    }
}
