using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Brand;
using System.Net;

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
        public async Task CreateBrand_WhenAllFieldsAreCorrect_ReturnOkResult()
        {
            //Arrange
            var brand = new CreateBrandDTO { Name = "TesteBrand" };
            var brandOutput = new BrandDTO { Id = Guid.NewGuid(), Name = "TesteBrand" };

            _fixture.BrandServiceMock.Setup(s => s.CreateAsync(brand)).ReturnsAsync(Result.SuccessWithMessage(""));

            //Act
            var result = await _fixture.Controller.Create(brand);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateBrand_WhenProductAlreadyExist_ReturnConflicResult()
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
        public async Task CreateBrand_WhenFieldsAreNotCorrect_ReturnBadRequestResult()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Create(brand));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }


    }
}
