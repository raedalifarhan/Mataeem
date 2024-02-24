using AutoMapper;
using Mataeem.DTOs.BusinessHoursDTOs;
using Mataeem.DTOs.CategoryDTOs;
using Mataeem.DTOs.CuisineDTOs;
using Mataeem.DTOs.DriverDTOs;
using Mataeem.DTOs.DriverRestaurantDTOs;
using Mataeem.DTOs.MenuDTOs;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.DTOs.RestaurantDTOs;
using Mataeem.Lib;
using Mataeem.Models;

namespace Mataeem.RequestHelpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Restaurant, RestaurantListDto>()
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => 
                    RestaurantHelper.IsRestaurantOpenNow(src.OpeningHours)));

            CreateMap<RestaurantSaveDto, Restaurant>();
            
            CreateMap<BusinessHours, BusinessHoursDto>();

            CreateMap<CategorySaveDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListDto>();

            CreateMap<CuisineSaveDto, Cuisine>();
            CreateMap<Cuisine, CuisineSaveDto>();
            CreateMap<Cuisine, CuisineDto>();
            CreateMap<Cuisine, CuisineListDto>();

            CreateMap<MenuSaveDto, Menu>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, MenuListDto>();

           CreateMap<Product, ProductDto>();
            CreateMap<ProductSaveDto, Product>();
            CreateMap<OptionDto, Product>();
            CreateMap<OptionValueDto, Product>();



            CreateMap<DriverRestaurant, DriverDto>()
                .IncludeMembers(x => x.Driver)
                .ForMember(dest => dest.AssignId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppUser, DriverDto>();



            CreateMap<DriverRestaurant, RestaurantDto>()
                .IncludeMembers(x => x.Restaurant)
                .ForMember(dest => dest.AssignId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Restaurant, RestaurantDto>();


        }
    }

}