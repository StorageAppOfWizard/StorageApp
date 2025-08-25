using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StorageProject.Tests.ProductControllerTest
{
    public class GetByIdProductTest : IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _fixture;

        public GetByIdProductTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetByIdProduct_OkResult()
        {
            //Arrange
            var ProductId = Guid.NewGuid();
            var fakeProduct = new ProductDTO
            {
                Id = ProductId,
                Name = "Brand1",
                BrandId = Guid.NewGuid(),
                BrandName = "Test",
                CategoryId = Guid.NewGuid(),
                CategoryName = "Test",
                Description = "Test",
                Quantity = 1,
                Status = ProductStatus.LowStock,
            };

            _fixture.ProductServiceMock.Setup(s => s.GetByIdAsync(ProductId)).ReturnsAsync(Result.Success(fakeProduct));

            //Act
            var result = await _fixture.Controller.GetById(ProductId);
            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetByIdProduct_NotFoundResult()
        {
            //Arrange
            var ProductId = Guid.NewGuid();

            _fixture.ProductServiceMock.Setup(s => s.GetByIdAsync(ProductId)).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.GetById(ProductId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);


        }

        [Fact]
        public async Task GetByIdProduct_ReturnInternalServerError()
        {
            //Arrange
            var ProductId = Guid.NewGuid();
            _fixture.ProductServiceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var result = await _fixture.Controller.GetById(ProductId);
            //Assert

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
