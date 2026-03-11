using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using StorageProject.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageProject.Application
{
    public class MessageConnection : IMessageConnection
    {

        private IConnection _connection;
        private ConnectionFactory _factory;

        public MessageConnection()
        {
        }


        public void ConnectionMessage(IConfiguration configuration)
        {
            _factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"],
                Port = int.Parse(configuration["RabbitMQ:Port"]),
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:VirtualHost"],
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),

            };
        }

        public async Task InitializeAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

    }
}
