using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageProject.Tests.CategoryControllerTest
{
    public class GetByIdCategoryTest : IClassFixture<CategoryControllerFixture>
    {
        private readonly CategoryControllerFixture _fixture;
        public GetByIdCategoryTest(CategoryControllerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetByIdCategory_OkResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new CategoryDTO { Id = categoryId, Name = "TestCategory" };
            _fixture.CategoryServiceMock.Setup(s => s.GetByIdAsync(categoryId)).ReturnsAsync(Result.Success(category));
            // Act
            var result = await _fixture.Controller.GetById(categoryId);
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetByIdCategory_NotFoundResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _fixture.CategoryServiceMock.Setup(s => s.GetByIdAsync(categoryId)).ReturnsAsync(Result.NotFound());
            // Act
            var result = await _fixture.Controller.GetById(categoryId);
            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetByIdCategory_InternalServerErrorResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _fixture.CategoryServiceMock.Setup(s => s.GetByIdAsync(categoryId)).ThrowsAsync(new Exception("Unexpected Error"));
            // Act
            var result = await _fixture.Controller.GetById(categoryId);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
