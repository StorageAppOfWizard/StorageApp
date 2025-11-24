using Moq;
using StorageProject.Api.Controllers;
using StorageProject.Application.Contracts;

namespace StorageProject.Tests.Controller.OrderControllerTest
{
    public class OrderControllerFixture
    {
        public Mock<IOrderService> OrderServiceMock { get; } = new();
        public OrderController Controller { get; }

        public OrderControllerFixture()
        {
            Controller = new OrderController(OrderServiceMock.Object);
        }
    }
}
