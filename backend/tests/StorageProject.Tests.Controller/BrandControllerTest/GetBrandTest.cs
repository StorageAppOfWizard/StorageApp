using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Domain.Entity;

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
        public async Task GetAllBrands_WhenBrandsExist_ReturnOkResult()
        {
            //Arrange
            var fakeList = new List<BrandDTO> { 
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand1"},
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand2"},
                new BrandDTO { Id = Guid.NewGuid(), Name = "Brand3"},
            };

            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Success(fakeList));
            //Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200,objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsDoesNotExist_ReturnNotFoundResult()
        {
            //Arrange
            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.NotFound("Brands Not Found"));
            //Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());
            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetAllBrands_ReturnInternalServerError()
        {
            //Arrange
            _fixture.BrandServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>()));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
