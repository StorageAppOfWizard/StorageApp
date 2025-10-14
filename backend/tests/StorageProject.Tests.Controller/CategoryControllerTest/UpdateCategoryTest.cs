using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.CategoryControllerTest
{
    public class UpdateCategoryTest : IClassFixture<CategoryControllerFixture>
    {
        private readonly CategoryControllerFixture _fixture;
        public UpdateCategoryTest(CategoryControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryExistsAndFieldsAreCorrect_ReturnOkResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateCategoryDto = new UpdateCategoryDTO { Id = categoryId, Name = "UpdatedCategory" };
            var updatedCategory = new CategoryDTO { Id = categoryId, Name = "UpdatedCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.UpdateAsync(updateCategoryDto)).ReturnsAsync(Result.Success());
            // Act
            var result = await _fixture.Controller.Update(updateCategoryDto);
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryAlreadyExist_ReturnConflictResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateCategoryDto = new UpdateCategoryDTO { Id = categoryId, Name = "UpdatedCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.UpdateAsync(updateCategoryDto)).ReturnsAsync(Result.Conflict());
            // Act
            var result = await _fixture.Controller.Update(updateCategoryDto);
            // Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_WhenFieldsAreNotCorrect_ReturnBadRequestResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateCategoryDto = new UpdateCategoryDTO { Id = categoryId, Name = "10" }; // Invalid name
            _fixture.CategoryServiceMock.Setup(s => s.UpdateAsync(updateCategoryDto)).ReturnsAsync(Result.Invalid());
            // Act
            var result = await _fixture.Controller.Update(updateCategoryDto);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryDoesNotExist_ReturnNotFoundResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateCategoryDto = new UpdateCategoryDTO { Id = categoryId, Name = "UpdatedCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.UpdateAsync(updateCategoryDto)).ReturnsAsync(Result.NotFound());
            // Act
            var result = await _fixture.Controller.Update(updateCategoryDto);
            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_ReturnInternalServerErrorResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateCategoryDto = new UpdateCategoryDTO { Id = categoryId, Name = "UpdatedCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.UpdateAsync(updateCategoryDto)).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Update(updateCategoryDto));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
