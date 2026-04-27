using StorageProject.Application.Contracts;
using StorageProject.Application.Services;
using StorageProject.Domain.Contracts;
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
            
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextAuth, UserContextAuth>();
            services.AddScoped<IMessagePublisher, MessagePublisher>();

        }
    }
}