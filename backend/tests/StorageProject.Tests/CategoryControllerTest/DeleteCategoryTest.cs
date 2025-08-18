using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace StorageProject.Tests.CategoryControllerTest
{
    public class DeleteCategoryTest : IClassFixture<CategoryControllerFixture>
    {
        private readonly CategoryControllerFixture _fixture;
        public DeleteCategoryTest(CategoryControllerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task DeleteCategory_OkResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _fixture.CategoryServiceMock.Setup(s => s.RemoveAsync(categoryId)).ReturnsAsync(Result.Success());
            // Act
            var result = await _fixture.Controller.Delete(categoryId);
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCategory_NotFoundResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _fixture.CategoryServiceMock.Setup(s => s.RemoveAsync(categoryId)).ReturnsAsync(Result.NotFound());
            // Act
            var result = await _fixture.Controller.Delete(categoryId);
            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }


        [Fact]
        public async Task DeleteCategory_InternalServerErrorResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _fixture.CategoryServiceMock.Setup(s => s.RemoveAsync(categoryId)).ThrowsAsync(new Exception("Unexpected Error"));
            // Act
            var result = await _fixture.Controller.Delete(categoryId);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

    }
}
