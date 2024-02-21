using Mataeem.Data;
using Mataeem.Data.Repositories;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Mataeem.Services;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("DefConn"));
            });           

            services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);

            //services.AddFluentValidationAutoValidation();

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IRestaurantRepository), typeof(RestaurantRepository));
            services.AddScoped(typeof(IBusinessHoursRepository), typeof(BusinessHoursRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IMenuRepository), typeof(MenuRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));


            return services;
        }
    }
}