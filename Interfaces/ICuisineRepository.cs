using Mataeem.DTOs.CuisineDTOs;

namespace Mataeem.Interfaces
{
    public interface ICuisineRepository
    {
        Task<bool> ReleaseRelationBetweenRestaurantAndCuisine(Guid restaurantId, Guid cuisineId);
        Task<bool> RelateRestauratnWithCuisine(Guid restaurantId, Guid cuisineId);

        Task<bool> CreateCuisine(CuisineSaveDto model);
        Task<bool> UpdateCuisine(Guid id, CuisineSaveDto model);
        Task<bool> DeleteCuisine(Guid id);
    }
}
