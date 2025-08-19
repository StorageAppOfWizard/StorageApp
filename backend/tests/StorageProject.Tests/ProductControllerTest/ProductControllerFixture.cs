using Moq;
using StorageProject.Api.Controllers;
using StorageProject.Application.Contracts;

namespace StorageProject.Tests.ProductControllerTest
{
    public class ProductControllerFixture
    {
        public Mock<IProductService> ProductServiceMock { get; } = new();
        public ProductController Controller { get; protected set; }

        public ProductControllerFixture()
        {
            Controller = new ProductController(ProductServiceMock.Object);
        }
    }
}
