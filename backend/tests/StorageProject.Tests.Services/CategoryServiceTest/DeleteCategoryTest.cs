using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class DeleteCategoryTest :IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        public DeleteCategoryTest(CategoryServiceFixture fixture)
        {
            _fixture = fixture;
        }



        [Fact]
        public async Task DeleteCategory_WhenIdIsAvailable_DeleteCategory()
        {
            //Arrange
            var category = new Category { Id = Guid.NewGuid(), Name ="Teste"};   
            
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(category.Id, cancellationToken)).ReturnsAsync(category);

            //Act
            var result = await _fixture.Service.RemoveAsync(category.Id);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Fact]
        public async Task DeleteCategory_WhenIdIsUnavailable_ErrorDeleteCategory()
        {
            //Arrange
            var category = new Category { Id = Guid.NewGuid(), Name = "Teste"};

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(category.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.RemoveAsync(category.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
