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
            var input = new CreateCategoryDTO { Name = "TestCategory" };
            var output = new CategoryDTO { Id = Guid.NewGuid(), Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(Result.Success(output));
            // Act
            var result = await _fixture.Controller.Create(input);
            // Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, objectResult.StatusCode);
        }
        [Fact]
        public async Task CreateCategory_ConflictResult()
        {
            // Arrange
            var input = new CreateCategoryDTO { Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(Result.Conflict());
            // Act
            var result = await _fixture.Controller.Create(input);
            // Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }
        [Fact]
        public async Task CreateCategory_BadRequestResult()
        {
            // Arrange
            var input = new CreateCategoryDTO { Name = "10" }; // Invalid name
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(Result.Invalid());
            // Act
            var result = await _fixture.Controller.Create(input);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
