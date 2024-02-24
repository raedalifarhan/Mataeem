using AutoMapper;
using Mataeem.DTOs.CuisineDTOs;
using Mataeem.Interfaces;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class CuisineRepository : ICuisineRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CuisineRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<bool> RelateRestauratnWithCuisine(Guid restaurantId, Guid cuisineId)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
                return false;

            var cuisine = await _context.Cuisines.FindAsync(cuisineId);
            if (cuisine == null)
                return false;

            if (await _context.RestaurantCuisines
                .AnyAsync(rc => rc.RestaurantId == restaurantId && rc.CuisineId == cuisineId))
                return false;

            var restaurantCuisine = new RestaurantCuisine
            {
                RestaurantId = restaurantId,
                CuisineId = cuisineId
            };

            _context.RestaurantCuisines.Add(restaurantCuisine);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReleaseRelationBetweenRestaurantAndCuisine(Guid restaurantId, Guid cuisineId)
        {
            var restaurantCuisine = await _context.RestaurantCuisines.FirstOrDefaultAsync(rc => rc.RestaurantId == restaurantId && rc.CuisineId == cuisineId);

            if (restaurantCuisine == null)
                return false;

            _context.RestaurantCuisines.Remove(restaurantCuisine);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateCuisine(CuisineSaveDto model)
        {
            if (model == null) return false;

            var Cuisine = _mapper.Map<Cuisine>(model);

            if (Cuisine == null) return false;

            Cuisine.CreateDate = DateTime.Now;
            Cuisine.IsActive = true;

            _context.Cuisines.Add(Cuisine);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> UpdateCuisine(Guid id, CuisineSaveDto model)
        {
            if (id == Guid.Empty) return false;

            var Cuisine = await _context.Cuisines.FindAsync(id);

            if (Cuisine == null) return false;

            _mapper.Map(model, Cuisine);

            Cuisine.LastUpdateDate = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

        public async Task<bool> DeleteCuisine(Guid id)
        {
            if (id == Guid.Empty) return false;

            var Cuisine = await _context.Cuisines
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (Cuisine == null) return false;

            if (!Cuisine.Products!.Any())
            {
                _context.Cuisines.Remove(Cuisine);
            }
            else
            {
                Cuisine.LastUpdateDate = DateTime.Now;
                Cuisine.IsActive = false;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }

    }
}
