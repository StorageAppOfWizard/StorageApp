using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.ProductControllerTest
{
    public class GetProductsTest : IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _fixture;

        public GetProductsTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult()
        {
            //Arrange
            var fakeList = new List<ProductDTO> {
                new ProductDTO {
                    Id = Guid.NewGuid(),
                    Name = "Brand1",
                    BrandId=Guid.NewGuid(),
                    BrandName="Test",
                    CategoryId=Guid.NewGuid(),
                    CategoryName="Test",
                    Description="Test",
                    Quantity=1,
                    Status=ProductStatus.LowStock,
                },
                new ProductDTO {
                    Id = Guid.NewGuid(),
                    Name = "Brand1",
                    BrandId=Guid.NewGuid(),
                    BrandName="Test",
                    CategoryId=Guid.NewGuid(),
                    CategoryName="Test",
                    Description="Test",
                    Quantity=1,
                    Status=ProductStatus.LowStock,
                },
            };

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Success(fakeList));

            //Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());

            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200,objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsNotFound()
        {
            //Arrange
            var fakeList = new List<ProductDTO> {
            };

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.NotFound("Products Not Found"));

            //Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetAllProducts_ReturnsInternalServerError()
        {

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>()));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
