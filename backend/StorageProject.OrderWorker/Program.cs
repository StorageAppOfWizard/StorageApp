using StorageProject.Application.Contracts;
using StorageProject.Application.Handlers;
using StorageProject.OrderWorker;
using StorageProject.OrderWorker.Contracts;
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
builder.Services.AddSingleton<IMessageTopology, MessageTopology>();
builder.Services.AddSingleton<IQueueDispatchHandler, QueueDispatchHandler>();

var host = builder.Build();
host.Run();
