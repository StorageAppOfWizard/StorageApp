using MassTransit;
using RabbitMQ.Client;
using StorageProject.Application.Handler;
using StorageProject.Domain.Entity.Messages.Order;

namespace StorageProject.Api.Configurations
{
    public static class MessageBrokerConfiguration
    {
        public static void AddMessageBrokerConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(Program).Assembly);
                x.AddConsumer<OrderCreatedMessageHandler>();
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
                        e.ConfigureConsumer<OrderCreatedMessageHandler>(context);
                    });

                    cfg.Message<OrderCreatedMessage>(m => m.SetEntityName("order-created"));
                });

            });
        }
    }
}
