using Moq;
using StorageProject.Application.Services;
using StorageProject.Domain.Contracts;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class ProductServiceFixture
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();
        public ProductService Service { get; protected set; }

        public ProductServiceFixture()
        {
            Service = new ProductService(UnitOfWorkMock.Object);
        }
    }
}