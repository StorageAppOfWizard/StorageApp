using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entities.Enums;

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

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync()).ReturnsAsync(Result.Success(fakeList));

            //Act
            var result = await _fixture.Controller.Get();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200,objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsNotFound()
        {
            //Arrange
            var fakeList = new List<ProductDTO> {
            };

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync()).ReturnsAsync(Result.NotFound("Products Not Found"));

            //Act
            var result = await _fixture.Controller.Get();

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetAllProducts_ReturnsInternalServerError()
        {

            _fixture.ProductServiceMock.Setup(g => g.GetAllAsync()).ThrowsAsync(new Exception("An Error Ocurred"));

            //Act
            var result = await _fixture.Controller.Get();

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
