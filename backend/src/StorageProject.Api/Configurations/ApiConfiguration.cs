using FluentValidation;
using StorageProject.Application.Validators;
using System.Text.Json.Serialization;

namespace StorageProject.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                });
            });
        }
    }
}
