using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.ProductControllerTest
{
    public class UpdatePatchProductTest :IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _fixture;

        public UpdatePatchProductTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdatePatchQuantityProduct_ReturnOkResult()
        {
            //Arrange
            var product = new UpdateProductQuantityDTO { Id = Guid.NewGuid(), Quantity = 1 };

            _fixture.ProductServiceMock.Setup(p => p.UpdateQuantityAsync(product)).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.UpdateQuantity(product);

            //Arrange
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdatePatchQuantityProduct_ReturnBadRequestResult()
        {
            //Arrange
            var product = new UpdateProductQuantityDTO { Id = Guid.NewGuid(), Quantity = 1 };

            _fixture.ProductServiceMock.Setup(p => p.UpdateQuantityAsync(product)).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.UpdateQuantity(product);

            //Arrange
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdatePatchQuantityProduct_InternalServerErrorResult()
        {
            //Arrange
            var product = new UpdateProductQuantityDTO { Id = Guid.NewGuid(), Quantity = 1 };

            _fixture.ProductServiceMock.Setup(p => p.UpdateQuantityAsync(product)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.UpdateQuantity(product));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
