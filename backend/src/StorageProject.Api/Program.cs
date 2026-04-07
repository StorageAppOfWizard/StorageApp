using DotNetEnv;
using StorageProject.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddMessageBrokerConfiguration();
builder.Services.AddLogConfiguration(builder);

var app = builder.Build();
app.AddPipelineConfiguration();
app.Run();
