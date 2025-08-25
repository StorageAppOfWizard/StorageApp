using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Tests.ProductControllerTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageProject.Tests.ProductControllerTest
{
    public class DeleteProductTest :IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _fixture;

        public DeleteProductTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteProduct_WhenIdExist_ReturnOkResult()
        {
            //Arrange
            var ProductId = Guid.NewGuid();
            _fixture.ProductServiceMock.Setup(s => s.RemoveAsync(ProductId)).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.Delete(ProductId);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }


        [Fact]
        public async Task DeleteProduct_WhenIdDoesNotExist_NotFoundResult()
        {
            //Arrange
            var ProductId = Guid.NewGuid();
            _fixture.ProductServiceMock.Setup(s => s.RemoveAsync(ProductId)).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.Delete(ProductId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ReturnInternalServerError()
        {
            //Arrange
            var ProductId = Guid.NewGuid();
            _fixture.ProductServiceMock.Setup(s => s.RemoveAsync(ProductId)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var result = await _fixture.Controller.Delete(ProductId);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
