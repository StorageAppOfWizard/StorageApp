using StorageProject.Application.Contracts;
using StorageProject.Application.Handlers;
using StorageProject.Application.Services;
using StorageProject.Infrastructure.Authentication;

namespace StorageProject.Api.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderHandler, ApprovedHandler>();
            services.AddScoped<IOrderHandler, RejectHandler>();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextAuth, UserContextAuth>();

        }
    }
}