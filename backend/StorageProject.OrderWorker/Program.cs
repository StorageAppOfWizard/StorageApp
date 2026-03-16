using StorageProject.OrderWorker;
using StorageProject.OrderWorker.Contracts;
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
builder.Services.AddSingleton<IQueueDispatchHandler, QueueDispatchHandler>();

var host = builder.Build();
host.Run();
