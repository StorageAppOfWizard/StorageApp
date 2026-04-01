using MassTransit;
using StorageProject.OrderWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    //todos consumers herdando de IConsumer
    x.AddConsumers(typeof(Program).Assembly);


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
