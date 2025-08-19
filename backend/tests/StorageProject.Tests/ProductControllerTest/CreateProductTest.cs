using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Product;

namespace StorageProject.Tests.ProductControllerTest
{
    public class CreateProductTest : IClassFixture<ProductControllerFixture>

    {

        private readonly ProductControllerFixture _fixture;

        public CreateProductTest(ProductControllerFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task CreateProduct_ReturnOK()
        {
            //Arrange
            var input = new CreateProductDTO {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity= 100 
            };

            var output = new ProductDTO
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Quantity = input.Quantity,
                Description = input.Description,
                BrandId = input.BrandId,
                CategoryId = input.CategoryId,
                BrandName = "Test",
                CategoryName = "Test"
            };

            _fixture.ProductServiceMock.Setup(c => c.CreateAsync(input)).ReturnsAsync(Result.Success(output));

            //Act
            var result = await _fixture.Controller.Create(input);

            //Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, objectResult.StatusCode);  
        }

        [Fact]
        public async Task CreateProduct_ReturnBadRequest()
        {
            //Arrange
            var input = new CreateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "",
                Name = "T",
                Quantity = 100
            };

            _fixture.ProductServiceMock.Setup(c => c.CreateAsync(input)).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.Create(input);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400,objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_ReturnConflict()
        {
            //Arrange
            var input = new CreateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity = 100
            };
            _fixture.ProductServiceMock.Setup(c => c.CreateAsync(input)).ReturnsAsync(Result.Conflict());

            //Act
            var result = await _fixture.Controller.Create(input);

            //Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_ReturnInternalServerErrorResult()
        {
            //Arrange
            var input = new CreateProductDTO
            {
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test",
                Name = "Test",
                Quantity = 100
            };
            _fixture.ProductServiceMock.Setup(c => c.CreateAsync(input)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var result = await _fixture.Controller.Create(input);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
