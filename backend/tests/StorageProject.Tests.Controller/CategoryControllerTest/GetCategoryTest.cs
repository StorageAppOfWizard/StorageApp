using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

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
        public async Task GetAllCategory_WhenCategoriesExist_ReturnOkResult()
        {
            // Arrange
            var fakeList = new List<CategoryDTO> {
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category1"},
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category2"},
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Category3"},
            };
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Success(fakeList));
            // Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetAllCategory_WhenCategoriesDoesNotExist_ReturnNotFoundResult()
        {
            // Arrange
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.NotFound());
            // Act
            var result = await _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>());
            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllCategory_ReturnInternalServerErrorResult()
        {
            // Arrange
            _fixture.CategoryServiceMock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Unexpected Error"));
            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Get(It.IsAny<int>(), It.IsAny<int>()));

            //Assert
            Assert.Equal("Unexpected Error", exception.Message);
        }
    }
}
