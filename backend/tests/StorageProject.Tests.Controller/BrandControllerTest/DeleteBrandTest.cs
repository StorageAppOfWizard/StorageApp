using Ardalis.Result;
using Microsoft.AspNetCore.Http;
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
        public async Task DeleteBrand_WhenIdExist_ReturnOkResult()
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
        public async Task DeleteBrand_WhenIdDoesNotExist_ReturnNotFoundResult()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Delete(brandId));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }

    }
}
