using DotNetEnv;
using OpenTelemetry.Logs;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using StorageProject.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var outputTemplate = "{Timestamp: [dd/MM/yyyy HH:mm:ss]} [{Level}] {SourceContext} - {Message:lj}{NewLine}{Exception}";


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: outputTemplate)
    .WriteTo.OpenTelemetry(
        endpoint: "localhost:4317",
        protocol: OtlpProtocol.HttpProtobuf)
    .CreateLogger();


builder.Host.UseSerilog();

builder.Logging.AddOpenTelemetry(o =>
{
    o.IncludeFormattedMessage = true;
    o.AddOtlpExporter();
});

var app = builder.Build();
app.AddPipelineConfiguration();
app.Run();
