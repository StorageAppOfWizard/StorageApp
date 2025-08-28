using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.ProductServiceTest
{
    public class UpdateProductTest : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public UpdateProductTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateProduct_WhenFieldsAreCorrect_ProductUpdated()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Marca Fake" };
            var category = new Category { Id = Guid.NewGuid(), Name = "Categoria Fake" };
            var dto = new UpdateProductDTO
            {
                Id = Guid.NewGuid(),
                Name = "TesteUpdate",
                BrandId = brand.Id,
                CategoryId = category.Id,
                Description = "",
                Quantity = 10
            };
            var newProduct = new Product { Id = dto.Id, Name = dto.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetByNameAsync(dto.Name, cancellationToken)) .ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(newProduct);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateProduct_WhenConflictExist_ErrorProductUpdated()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Marca Fake" };
            var category = new Category { Id = Guid.NewGuid(), Name = "Categoria Fake" };
            var dto = new UpdateProductDTO
            { 
                Id = Guid.NewGuid(),
                Name = "TesteUpdate",
                BrandId = brand.Id,
                CategoryId = category.Id,
                Description="",
                Quantity = 10
            };
            var existingProductName = new Product { Id = dto.Id, Name = dto.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(existingProductName);
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(existingProductName);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.Conflict, result.Status);
        }

        public async Task UpdateProduct_WhenProductNotExist_ErrorProductUpdated()
        {
            //Arrange
            var dto = new UpdateProductDTO { Id = Guid.NewGuid(), Name = "TesteUpdate" };

            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(It.IsAny<Product>);
            _fixture.UnitOfWorkMock.Setup(c => c.ProductRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a Product name that should be max 20 chars...")]
        public async Task UpdateProduct_WhenNameFieldIsIncorret_ErrorProductUpdated(string invalidName)
        {
            //Arrange
            var dto = new UpdateProductDTO { Name = invalidName, Id = Guid.NewGuid() };

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
        }


    }
}
