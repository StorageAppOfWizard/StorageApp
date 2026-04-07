using MassTransit;
using RabbitMQ.Client;
using StorageApp.Orders.Domain.Entity;

using StorageProject.OrderWorker;

var builder = Host.CreateApplicationBuilder(args);



var host = builder.Build();
host.Run();
