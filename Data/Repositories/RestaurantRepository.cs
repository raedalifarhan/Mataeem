using AutoMapper;
using Mataeem.DTOs.CategoryDTOs;
using Mataeem.DTOs.MenuDTOs;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.DTOs.RestaurantDTOs;
using Mataeem.Interfaces;
using Mataeem.Lib;
using Mataeem.Models;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly string _pictureBase;

        public RestaurantRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _pictureBase = "restaurants-images";
        }

        public async Task<List<RestaurantListDto>> GetAllRestaurants()
        {
            var query = await _context.Restaurants
                .Where(x => x.IsActive)
                .ToListAsync();

            return _mapper.Map<List<RestaurantListDto>>(query);
        }

        public async Task<RestaurantDetailsDto> GetRestaurant(Guid id)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Menu)
                .Where(r => r.Id == id)
                .Select(r => new RestaurantDetailsDto
                {
                    Id = r.Id,
                    RestaurantName = r.RestaurantName,
                    LocationLatitude = r.LocationLatitude ?? 0,
                    LocationLongitude = r.LocationLongitude ?? 0,
                    City = r.City ?? string.Empty,
                    District = r.District ?? string.Empty,
                    Address = r.Address ?? string.Empty,
                    ContactNumber = r.ContactNumber ?? string.Empty,
                    PhoneNumber = r.PhoneNumber ?? string.Empty,
                    IsOpen = RestaurantHelper.IsRestaurantOpenNow(r.OpeningHours),
                    Website = r.Website ?? string.Empty,
                    PictureUrl = r.PictureUrl ?? string.Empty,
                    Menu = new MenuDto
                    {
                        MenuName = r.Menu!.MenuName,
                        Categories = r.Menu.Categories!.Select(c => new CategoryDto
                        {
                            ProductName = c.CategoryName,
                            Products = _context.Products
                                .Where(r => r.CategoryId == c.Id)
                                .Select(p => new ProductDto
                                {
                                    ProductName = p.ProductName,
                                    Rate = p.Rate,
                                    PictureUrl = p.PictureUrl ?? string.Empty,
                                    Options = _context.Products.Where(x => x.ParentId == p.Id).Select(pr => new OptionDto
                                    {
                                        ProductName = pr.ProductName,
                                        Type = pr.OptionType,
                                        IsMandatory = pr.IsMandatory,
                                        MandatoryCount = 3,
                                        Values = _context.Products.Where(x => x.ParentId == pr.Id).Select(op => new OptionValueDto
                                        {
                                            ProductName = op.ProductName,
                                            RegularPrice = op.RegularPrice,
                                            SellingPrice = op.SellingPrice,
                                        }).ToList() ?? new List<OptionValueDto>()
                                    }).ToList() ?? new List<OptionDto>()
                                }).ToList()
                        }).ToList()
                    }
                })
            .FirstOrDefault();

            return restaurant ?? null!;
        }

        public async Task<bool> CreateRestaurant(RestaurantSaveDto model)
        {
            if (model == null) return false;

            var restaurant = _mapper.Map<Restaurant>(model);

            if (restaurant == null) return false;

            if (model.FormFile != null)
            {
                var fileName = await FileHelper.SaveFileToServer(model.FormFile, _pictureBase, restaurant.PictureUrl);

                if (fileName == null) return false;

                restaurant.PictureUrl = fileName;
            }

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

            if (model.FormFile != null)
            {
                var fileName = await FileHelper.SaveFileToServer(model.FormFile, _pictureBase, restaurant.PictureUrl);

                if (fileName == null) return false;

                restaurant.PictureUrl = fileName;
            }

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
