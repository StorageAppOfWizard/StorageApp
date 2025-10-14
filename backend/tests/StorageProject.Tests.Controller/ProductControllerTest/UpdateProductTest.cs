using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.ProductControllerTest
{
    public class UpdateProductTest : IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _fixture;

        public UpdateProductTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateProduct_ReturnOk()
        {
            //Arrange
            var input = new UpdateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity = 100
            };

            _fixture.ProductServiceMock.Setup(u => u.UpdateAsync(input)).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.Update(input);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnBadRequest()
        {
            //Arrange
            var input = new UpdateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "",
                Name = "T",
                Quantity = 100
            };

            _fixture.ProductServiceMock.Setup(c => c.UpdateAsync(input)).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.Update(input);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnConflict()
        {
            //Arrange
            var input = new UpdateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity = 100
            };
            _fixture.ProductServiceMock.Setup(c => c.UpdateAsync(input)).ReturnsAsync(Result.Conflict());

            //Act
            var result = await _fixture.Controller.Update(input);

            //Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnInternalServerErrorResult()
        {
            //Arrange
            var input = new UpdateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity = 100
            };
            _fixture.ProductServiceMock.Setup(c => c.UpdateAsync(input)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Update(input));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }


    }
}
