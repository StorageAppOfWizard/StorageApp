using StorageProject.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var app = builder.Build();

app.AddPipelineConfiguration();
app.Run();
