using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StorageProject.Application.DTOs.Messages;
using StorageProject.OrderWorker.Contracts.Configuration;
using StorageProject.OrderWorker.Contracts.Consumer;
using StorageProject.OrderWorker.Contracts.Handler;
using System.Text.Json;

namespace StorageProject.OrderWorker.Message
{
    public partial class MessageConsumer : IMessageConsumer
    {
        private readonly IMessageConnection _connection;
        private readonly IMessageConfiguration _topology;
        private readonly IConfiguration _configuration;
        private readonly IQueueDispatchHandler _queueDispatchHandler;

        public MessageConsumer(IMessageConnection connection, IMessageConfiguration topology, IConfiguration configuration, IQueueDispatchHandler queueDispatchHandler)
        {
            _connection = connection;
            _topology = topology;
            _configuration = configuration;
            _queueDispatchHandler = queueDispatchHandler;
        }

        public async Task ConsumerMessage(CancellationToken cancellationToken)
        {

            var channel = await _connection.GetConnection().CreateChannelAsync();
            await _topology.ConfigureAsync(channel);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<MessageEnvelope>(body);

                await (message.EventType switch
                {
                    "pedido.criado" => _queueDispatchHandler.DispatchAsync(message.Payload.Deserialize<OrderCreatedMessage>(), cancellationToken),

                }
                 );

                Console.WriteLine($" [x] Received {message} \n Routing Key: {ea.RoutingKey}");

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            await channel.BasicConsumeAsync
            (
                queue: _configuration["RabbitMqEnviroment:QueueName"],
                autoAck: false,
                consumer: consumer
            );
        }
    }
}
