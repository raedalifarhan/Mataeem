namespace Mataeem.Interfaces
{
    public interface IDriverRepository
    {
        Task<bool> AssignDriverToRestaurant(string driverId, Guid restaurantId);
        Task<bool> ReleaseDriverFromRestaurant(Guid id);
    }
}
