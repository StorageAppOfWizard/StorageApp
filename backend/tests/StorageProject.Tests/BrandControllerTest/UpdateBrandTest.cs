using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Tests.BrandControllerTest
{
    public class UpdateBrandTest : IClassFixture<BrandControllerFixture>
    {
        private readonly BrandControllerFixture _fixture;
        public UpdateBrandTest(BrandControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateBrand_OkResult()
        {
            //Arrange
            var brand = new UpdateBrandDTO { Name = "TesteBrand", Id = Guid.NewGuid() };

            _fixture.BrandServiceMock.Setup(s => s.UpdateAsync(brand)).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.Update(brand);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBrand_ConflicResult()
        {
            //Arrange
            var brand = new UpdateBrandDTO { Name = "TesteBrand" };
            _fixture.BrandServiceMock.Setup(s=>s.UpdateAsync(brand)).ReturnsAsync(Result.Conflict());

            //Act
            var result = await _fixture.Controller.Update(brand);

            //Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBrand_BadRequestResult()
        {
            //Arrange
            var brand = new UpdateBrandDTO { Name = "10", Id = Guid.NewGuid() };
            _fixture.BrandServiceMock.Setup(s => s.UpdateAsync(brand)).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.Update(brand);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBrand_NotFoundResult()
        {
            //Arrange
            var brand = new UpdateBrandDTO { Name = "TesteBrand", Id = Guid.NewGuid() };
            _fixture.BrandServiceMock.Setup(s => s.UpdateAsync(brand)).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.Update(brand);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBrand_ReturnInternalServerError()
        {
            //Arrange
            var brand = new UpdateBrandDTO { Name = "TesteBrand" };
            _fixture.BrandServiceMock.Setup(s => s.UpdateAsync(brand)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var result = await _fixture.Controller.Update(brand);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
