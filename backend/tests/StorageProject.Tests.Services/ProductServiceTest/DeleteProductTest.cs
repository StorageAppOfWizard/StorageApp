using Ardalis.Result;
using Moq;
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
            var Product = new Product { Id = Guid.NewGuid(), Name ="Teste"};   
            
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(Product.Id, cancellationToken)).ReturnsAsync(Product);

            //Act
            var result = await _fixture.Service.RemoveAsync(Product.Id);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Fact]
        public async Task DeleteProduct_WhenIdIsUnavailable_ErrorDeleteProduct()
        {
            //Arrange
            var Product = new Product { Id = Guid.NewGuid(), Name = "Teste"};

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(Product.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.RemoveAsync(Product.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
