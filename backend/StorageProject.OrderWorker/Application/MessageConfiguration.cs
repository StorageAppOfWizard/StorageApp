using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using StorageProject.OrderWorker.Contracts;

namespace StorageProject.OrderWorker.Message
{
    public class MessageConfiguration : IMessageConfiguration
    {
        private readonly IMessageConnection _connection;
        private readonly IConfiguration _configuration;

        public MessageConfiguration(IMessageConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public async Task ConfigureAsync(IChannel channel)
        {

            channel = await _connection.GetConnection().CreateChannelAsync();

            await channel.ExchangeDeclareAsync
            (
                exchange: _configuration["RabbitMqEnviroment:ExchangeName"],
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueDeclareAsync
            (
                queue: _configuration["RabbitMqEnviroment:QueueName"],
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueBindAsync
            (
                queue: _configuration["RabbitMqEnviroment:QueueName"],
                exchange: _configuration["RabbitMqEnviroment:ExchangeName"],
                routingKey: _configuration["RabbitMqEnviroment:RoutingKey"],
                arguments: null
            );

            await channel.BasicQosAsync
            (
                prefetchSize: 0,
                prefetchCount:1,
                global: false
            );

        }
    }
}
