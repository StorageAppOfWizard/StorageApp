using DotNetEnv;
using OpenTelemetry.Logs;
using StorageProject.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);


builder.Logging.AddOpenTelemetry(o =>
{     o.IncludeFormattedMessage = true;
    o.AddOtlpExporter();
});

var app = builder.Build();
app.AddPipelineConfiguration();
app.Run();
