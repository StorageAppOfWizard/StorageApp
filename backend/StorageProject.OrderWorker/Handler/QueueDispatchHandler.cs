using RabbitMQ.Client.Events;
using StorageProject.OrderWorker.Contracts;

namespace StorageProject.OrderWorker.Handler
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
