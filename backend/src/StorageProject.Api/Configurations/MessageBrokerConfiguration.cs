using MassTransit;
using RabbitMQ.Client;
using StorageApp.Orders.Domain.Entity;

namespace StorageProject.Api.Configurations
{
    public static class MessageBrokerConfiguration
    {
        public static void AddMessageBrokerConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
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
        }
    }
}
