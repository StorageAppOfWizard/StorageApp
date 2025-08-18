using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Tests.BrandControllerTest
{
    public class GetByIdBrandTest : IClassFixture<BrandControllerFixture>
    {
        private readonly BrandControllerFixture _fixture;
        public GetByIdBrandTest(BrandControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetByIdBrand_OkResult()
        {
            //Arrange
            var brandId = Guid.NewGuid();
            var fakeBrand = new BrandDTO { Id = brandId, Name = "Brand1" };

            _fixture.BrandServiceMock.Setup(s => s.GetByIdAsync(brandId)).ReturnsAsync(Result.Success(fakeBrand));

            //Act
            var result = await _fixture.Controller.GetById(brandId);
            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetByIdBrand_NotFoundResult()
        {
            //Arrange
            var brandId = Guid.NewGuid();

            _fixture.BrandServiceMock.Setup(s => s.GetByIdAsync(brandId)).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.GetById(brandId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);


        }

        [Fact]
        public async Task GetByIdBrand_ReturnInternalServerError()
        {
            //Arrange
            var brandId = Guid.NewGuid();
            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var result = await _fixture.Controller.GetById(brandId);
            //Assert

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
