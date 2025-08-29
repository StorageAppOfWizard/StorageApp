using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class GetAllProductsServiceTest : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetAllProductsServiceTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllProduct_WhenProductsExist_ReturnAllProducts()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Marca Fake" };
            var category = new Category { Id = Guid.NewGuid(), Name = "Categoria Fake" };
            var list = new List<Product> { 
                new Product { 
                    Name = "Teste",
                    Id = Guid.NewGuid(),
                    Brand = brand,
                    Category= category,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    Description = "",
                    Quantity = 10
                },
            };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetAllWithIncludesAsync(It.IsAny<int>(), It.IsAny<int>(), cancellationToken)).ReturnsAsync(list);

            //Act
            var result = await _fixture.Service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }

        [Fact]
        public async Task GetAllProduct_WhenProductsNoExist_ErrorReturnProducts()
        {
            //Arrange

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetAll(cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);

        }
    }
}
