using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;

namespace StorageProject.Tests.CategoryControllerTest
{
    public class CreateCategoryTest : IClassFixture<CategoryControllerFixture>
    {
        private readonly CategoryControllerFixture _fixture;
        public CreateCategoryTest(CategoryControllerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task CreateCategory_OkResult()
        {
            // Arrange
            var category = new CreateCategoryDTO { Name = "TestCategory" };
            var categoryOutput = new CategoryDTO { Id = Guid.NewGuid(), Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(category)).ReturnsAsync(Result.Success(categoryOutput));
            // Act
            var result = await _fixture.Controller.Create(category);
            // Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, objectResult.StatusCode);
        }
        [Fact]
        public async Task CreateCategory_ConflictResult()
        {
            // Arrange
            var category = new CreateCategoryDTO { Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(category)).ReturnsAsync(Result.Conflict());
            // Act
            var result = await _fixture.Controller.Create(category);
            // Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }
        [Fact]
        public async Task CreateCategory_BadRequestResult()
        {
            // Arrange
            var category = new CreateCategoryDTO { Name = "10" }; // Invalid name
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(category)).ReturnsAsync(Result.Invalid());
            // Act
            var result = await _fixture.Controller.Create(category);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
