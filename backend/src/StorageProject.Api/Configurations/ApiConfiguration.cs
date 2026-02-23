using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StorageProject.Application.Validators;
using System.Text;
using System.Text.Json.Serialization;

namespace StorageProject.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var key = config["Jwt:Key"] ?? "default-key";

            services.AddValidatorsFromAssemblyContaining<ProductValidator>();
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(key)),
                        NameClaimType = "unique_name",

                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
                options.AddPolicy("AdminOrManager", policy => policy.RequireRole("Manager", "Admin"));
            });

            services.AddOpenTelemetry()
                .ConfigureResource(r => r.AddService("ProductApi"))
                .WithTracing(t =>
                {
                    t.AddAspNetCoreInstrumentation()
                    .AddOtlpExporter();
                })
                .WithLogging(l => l.AddOtlpExporter())
                .WithMetrics(m=>m.AddMeter());

            services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));



        }
    }
}
