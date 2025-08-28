using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class CreateProductTest : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;

        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public CreateProductTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task CreateProduct_WhenFieldsAreCorrect_ProductCreated()
        {
            //Arrange
            //to do: Product Service Tests
            var dto = new CreateProductDTO
            { 
                Name = "Teste",
                BrandId = Guid.NewGuid(), 
                CategoryId = Guid.NewGuid(),
                Description="",
                Quantity=10,
            };

            var newProduct = new Product { 
                Id = Guid.NewGuid(),
                Name = dto.Name,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Quantity = dto.Quantity,  
            };


            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.Create(It.IsAny<Product>(), cancellationToken)).ReturnsAsync(newProduct);
            _fixture.UnitOfWorkMock.Setup(c => c.CommitAsync());

            //Act
            var result = await _fixture.Service.CreateAsync(dto);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a Product name that should be max 20 chars...")]

        public async Task CreateProduct_WhenNameFieldIsIncorret_ErrorProductCreated(string invalidName)
        {
            //Arrange
            var dto = new CreateProductDTO { Name = invalidName };

            //Act
            var result = await _fixture.Service.CreateAsync(dto); 
           
            //Assert
            Assert.False(result.IsSuccess);
        }







    }
}
