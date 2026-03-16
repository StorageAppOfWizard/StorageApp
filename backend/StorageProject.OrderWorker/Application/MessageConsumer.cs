using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StorageProject.Application.Handlers;
using StorageProject.OrderWorker.Contracts;
using System.Text;

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

        public async Task ConsumerMessage()
        {

            var channel = await _connection.GetConnection().CreateChannelAsync();
            await _topology.ConfigureAsync(channel);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body =  ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await _queueDispatchHandler.DispatchHandler(message,ea);

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
