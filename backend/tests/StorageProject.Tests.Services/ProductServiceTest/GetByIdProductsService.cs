using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class GetByIdProductsService :IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetByIdProductsService(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GuidProductById_WhenIdExist_ReturnProduct()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Marca Fake" };
            var category = new Category { Id = Guid.NewGuid(), Name = "Categoria Fake" };
            var product = new Product {
                    Name = "Teste",
                    Id = Guid.NewGuid(),
                    Brand = brand,
                    Category= category,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    Description = "",
                    Quantity = 10
            };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetByIdWithIncludesAsync(product.Id, cancellationToken)).ReturnsAsync(product);

            //Act
            var result = await _fixture.Service.GetByIdAsync(product.Id);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }
        
        [Fact]
        public async Task GuidProductById_WhenIdNoExist_ErrorReturnProduct()
        {
            //Arrange
            var product = new Product { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(product.Id, cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetByIdAsync(product.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }


    }
}
