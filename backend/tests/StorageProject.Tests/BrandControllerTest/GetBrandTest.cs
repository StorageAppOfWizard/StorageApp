using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Tests.BrandControllerTest
{
    public class GetBrandTest : IClassFixture<BrandControllerFixture>
    {
        private readonly BrandControllerFixture _fixture;

        public GetBrandTest(BrandControllerFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task GetAllBrands_OkResult()
        {
            //Arrange
            var fakeList = new List<BrandDTO> { 
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand1"},
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand2"},
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand3"},
            };

            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(Result.Success(fakeList));
            //Act
            var result = await _fixture.Controller.Get();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200,objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllBrands_NotFoundResult()
        {
            //Arrange
            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(Result.NotFound("Brands Not Found"));
            //Act
            var result = await _fixture.Controller.Get();
            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetAllBrands_ReturnInternalServerError()
        {
            //Arrange
            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var result = await _fixture.Controller.Get();
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
