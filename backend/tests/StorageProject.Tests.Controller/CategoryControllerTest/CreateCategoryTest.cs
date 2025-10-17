using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

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
        public async Task CreateCategory_WhenAllFieldsAreCorrect_ReturnOkResult()
        {
            // Arrange
            var input = new CreateCategoryDTO { Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(Result.SuccessWithMessage($"{input.Name} created!"));
            // Act
            var result = await _fixture.Controller.Create(input);
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async Task CreateCategory_WhenProductAlreadyExist_ReturnConflictResult()
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
        public async Task CreateCategory_WhenFieldsAreNotCorrect_ReturnBadRequestResult()
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

        [Fact]
        public async Task CreateCategory_ReturnInternalServerError()
        {
            var input = new CreateCategoryDTO { Name = "Teste" };
            _fixture.CategoryServiceMock.Setup(s => s.CreateAsync(input)).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Create(input));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
