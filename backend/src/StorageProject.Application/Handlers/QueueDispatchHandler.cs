using RabbitMQ.Client.Events;
using StorageProject.Application.Contracts;

namespace StorageProject.Application.Handlers
{
    public class QueueDispatchHandler : IQueueDispatchHandler
    {

        const string pedidoRoutingKeyName = "pedido.criado";
        public async Task DispatchHandler(string message, BasicDeliverEventArgs eventArgs)
        {
            switch (eventArgs.RoutingKey)
            {
                case pedidoRoutingKeyName: 
                    Console.WriteLine("Pedido processado");
                    break;
            }
        }
    }
}
