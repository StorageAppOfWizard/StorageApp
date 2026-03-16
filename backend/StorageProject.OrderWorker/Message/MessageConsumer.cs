using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StorageProject.OrderWorker.Contracts;
using System.Text;

namespace StorageProject.OrderWorker.Message
{
    public partial class MessageConsumer : IMessageConsumer
    {
        private readonly IMessageConnection _connection;
        private readonly IMessageTopology _topology;
        private readonly IConfiguration _configuration;

        public MessageConsumer(IMessageConnection connection, IMessageTopology topology, IConfiguration configuration)
        {
            _connection = connection;
            _topology = topology;
            _configuration = configuration;
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
                Console.WriteLine($" [x] Received {message}");
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
