
using System.Text;
using Mataeem.Data;
using Mataeem.Helpers;
using Mataeem.Models;
using Mataeem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Mataeem.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt => {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();
            //.AddDefaultTokenProviders();

            services.AddAuthentication();
            services.Configure<JWT>(config.GetSection("JWT"));
            services.AddScoped<AuthService>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddCors(opt =>
            {
                opt.AddPolicy("Access-Control-Allow-Origin", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200", "http://localhost:3000");
                });
            });

            return services;
        }
    }
}