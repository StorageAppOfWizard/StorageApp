using StorageProject.Application.DTOs.Messages;
using StorageProject.Domain.Contracts;
using StorageProject.Infrastructure.Repositories;
using StorageProject.OrderWorker;
using StorageProject.OrderWorker.Application.Configuration;
using StorageProject.OrderWorker.Contracts.Configuration;
using StorageProject.OrderWorker.Contracts.Consumer;
using StorageProject.OrderWorker.Contracts.Handler;
using StorageProject.OrderWorker.Handler;
using StorageProject.OrderWorker.Message;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IMessageConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var messageConnection = new MessageConnection();
    messageConnection.ConnectionMessage(config);
    messageConnection.InitializeAsync().GetAwaiter().GetResult(); // inicializa sync no startup
    return messageConnection;
});


builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
builder.Services.AddSingleton<IMessageConfiguration, MessageConfiguration>();
builder.Services.AddSingleton<IMessageHandler<OrderCreatedMessage>, OrderCreatedHandler>();
builder.Services.AddSingleton<IQueueDispatchHandler, QueueDispatcherHandler>();
// colocar DI UnitOfWork 
// ver mais sobre IServiceScopeFactory 

var host = builder.Build();
host.Run();
