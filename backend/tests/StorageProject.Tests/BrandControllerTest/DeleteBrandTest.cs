using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace StorageProject.Tests.BrandControllerTest
{
    public class DeleteBrandTest : IClassFixture<BrandControllerFixture>
    {
        private readonly BrandControllerFixture _fixture;

        public DeleteBrandTest(BrandControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteBrand_OkResult()
        {
            //Arrange
            var brandId= Guid.NewGuid();
            _fixture.BrandServiceMock.Setup(s => s.RemoveAsync(brandId)).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.Delete(brandId);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }


        [Fact]
        public async Task DeleteBrand_NotFoundResult()
        {
            //Arrange
            var brandId = Guid.NewGuid();
            _fixture.BrandServiceMock.Setup(s => s.RemoveAsync(brandId)).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.Delete(brandId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteBrand_ReturnInternalServerError()
        {
            //Arrange
            var brandId = Guid.NewGuid();
            _fixture.BrandServiceMock.Setup(s => s.RemoveAsync(brandId)).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var result = await _fixture.Controller.Delete(brandId);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

    }
}
