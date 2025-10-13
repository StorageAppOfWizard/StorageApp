using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StorageProject.Api.Middlewares;
using StorageProject.Application.Contracts;
using StorageProject.Application.Security;
using StorageProject.Application.Services;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;
using StorageProject.Infrasctructure.Data;
using StorageProject.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
    
var connectionString = builder.Configuration.GetConnectionString("StorageContext");


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IHashPassword, HashPassword>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors("AllowSpecificOrigins");
}



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<MiddlewareException>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
