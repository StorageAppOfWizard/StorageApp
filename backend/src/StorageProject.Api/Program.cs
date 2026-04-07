using DotNetEnv;
using MassTransit;
using OpenTelemetry.Logs;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using StorageApp.Orders.Domain.Entity;
using StorageProject.Api;
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

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CurrentMessageConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("storage-order-queue", e =>
        {
            e.Bind("order-created", x =>
            {
                x.ExchangeType = ExchangeType.Fanout;
            });

            e.ConfigureConsumer<CurrentMessageConsumer>(context);
        });

        cfg.Message<OrderMessage>(m => m.SetEntityName("order-created"));
    });

});

var app = builder.Build();
app.AddPipelineConfiguration();
app.Run();
