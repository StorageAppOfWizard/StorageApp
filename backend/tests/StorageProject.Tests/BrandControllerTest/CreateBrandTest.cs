using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Tests.BrandControllerTest
{
    public class CreateBrandTest : IClassFixture<BrandControllerFixture>
    {
        private readonly BrandControllerFixture _fixture;

        public CreateBrandTest(BrandControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateBrand_OkResult()
        {
            //Arrange
            var brand = new CreateBrandDTO { Name = "TesteBrand" };
            var brandOutput = new BrandDTO { Id = Guid.NewGuid(), Name = "TesteBrand" };

            _fixture.BrandServiceMock.Setup(s => s.CreateAsync(brand)).ReturnsAsync(Result.Success(brandOutput));

            //Act
            var result = await _fixture.Controller.Create(brand);

            //Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateBrand_ConflicResult()
        {
            //Arrange
            var brand = new CreateBrandDTO { Name = "TesteBrand" };
            _fixture.BrandServiceMock.Setup(s => s.CreateAsync(brand)).ReturnsAsync(Result.Conflict());

            //Act
            var result = await _fixture.Controller.Create(brand);

            //Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateBrand_BadRequestResult()
        {
            //Arrange
            var brand = new CreateBrandDTO { Name = "10" };
            _fixture.BrandServiceMock.Setup(s => s.CreateAsync(brand)).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.Create(brand);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateBrand_ReturnInternalServerError()
        {
            //Arrange
            var brand = new CreateBrandDTO { Name = "TesteBrand" };
            _fixture.BrandServiceMock.Setup(s => s.CreateAsync(brand)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var result = await _fixture.Controller.Create(brand);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }


    }
}
