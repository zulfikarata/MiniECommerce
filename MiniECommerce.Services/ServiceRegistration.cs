using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.DAL.Concretes;
using MiniECommerce.DAL.Contexts;
using MiniECommerce.Services.Abstracts;
using MiniECommerce.Services.Concrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MiniECommerceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

            services.AddIdentity<User, IdentityRole<Guid>>()
              .AddEntityFrameworkStores<MiniECommerceDbContext>()
              .AddDefaultTokenProviders();
            //Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}
