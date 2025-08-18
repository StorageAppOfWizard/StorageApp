using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;

namespace StorageProject.Tests.CategoryControllerTest
{
    public class GetCategoryTest : IClassFixture<CategoryControllerFixture>
    {
        private readonly CategoryControllerFixture _fixture;
        public GetCategoryTest(CategoryControllerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetAllCategory_OkResult()
        {
            // Arrange
            var fakeList = new List<CategoryDTO> {
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category1"},
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category2"},
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category3"},
            };
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(Result.Success(fakeList));
            // Act
            var result = await _fixture.Controller.Get();
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetAllCategory_NotFoundResult()
        {
            // Arrange
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(Result.NotFound());
            // Act
            var result = await _fixture.Controller.Get();
            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllCategory_InternalServerErrorResult()
        {
            // Arrange
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Unexpected Error"));
            // Act
            var result = await _fixture.Controller.Get();
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
