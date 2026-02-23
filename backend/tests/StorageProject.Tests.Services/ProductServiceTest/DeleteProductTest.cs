using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class DeleteProductTest :IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        public DeleteProductTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteProduct_WhenIdIsAvailable_DeleteProduct()
        {
            //Arrange
            var product = new Product { Id = Guid.NewGuid(), Name ="Teste"};
            
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(product.Id, cancellationToken)).ReturnsAsync(product);
            _fixture.UnitOfWorkMock.Setup(c => c.OrderRepository.GetAll(cancellationToken)).ReturnsAsync(value: []);

            //Act
            var result = await _fixture.Service.RemoveAsync(product.Id);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Fact]
        public async Task DeleteProduct_WhenIdIsUnavailable_ErrorDeleteProduct()
        {
            //Arrange
            var product = new Product { Id = Guid.NewGuid(), Name = "Teste" };
            var order = new Order { Id = Guid.NewGuid(), ProductId = product.Id, UserId = "idVálido", QuantityProduct = 10 };
            order.UpdateStatus(OrderStatus.Reject);

            var orders = new List<Order> { order };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(product.Id, cancellationToken)).ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(c => c.OrderRepository.GetAll(cancellationToken)).ReturnsAsync(orders);

            //Act
            var result = await _fixture.Service.RemoveAsync(product.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }

        [Fact]
        public async Task DeleteProduct_WhenExistPendingOrderTied_ErrorDeleteProduct()
        {
            //Arrange
            var product = new Product { Id = Guid.NewGuid(), Name = "Teste"};
            var orders =
                new List<Order>
                {
                    new Order { Id = Guid.NewGuid(), ProductId = product.Id, UserId = "idVálido", QuantityProduct = 10 }
                };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(product.Id, cancellationToken)).ReturnsAsync(product);
            _fixture.UnitOfWorkMock.Setup(c => c.OrderRepository.GetAll(cancellationToken)).ReturnsAsync(orders);

            //Act
            var result = await _fixture.Service.RemoveAsync(product.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.Error, result.Status);
        }
    }
}
