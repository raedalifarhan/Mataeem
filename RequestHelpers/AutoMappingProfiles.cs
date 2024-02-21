using AutoMapper;
using Mataeem.DTOs.BusinessHoursDTOs;
using Mataeem.DTOs.CategoryDTOs;
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

            CreateMap<MenuSaveDto, Menu>();
            CreateMap<Menu, MenuListDto>();
            CreateMap<Menu, MenuDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductListDto>();

            CreateMap<Product, OptionDto>();

            CreateMap<Product, OptionValueDto>();

        }
    }

}