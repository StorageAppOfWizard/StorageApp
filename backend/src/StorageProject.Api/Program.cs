using StorageProject.Api.Configurations;
using StorageProject.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.Services.ApplyMigrations();


app.AddPipelineConfiguration();
app.Run();
