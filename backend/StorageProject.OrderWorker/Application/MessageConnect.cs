using RabbitMQ.Client;
using StorageProject.OrderWorker.Contracts;

namespace StorageProject.OrderWorker.Message
{
    public class MessageConnection : IMessageConnection
    {

        private IConnection _connection;
        private ConnectionFactory _factory;

        public MessageConnection()
        {
        }


        public async void ConnectionMessage(IConfiguration configuration)
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
                
                Ssl =
                {
                    ServerName = configuration["RabbitMQ:HostName"],
                    Enabled= false
                }

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
