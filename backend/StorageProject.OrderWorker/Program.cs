using MassTransit;
using StorageProject.Application.DTOs.Messages;
using StorageProject.OrderWorker;

var builder = Host.CreateApplicationBuilder(args);

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

        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
