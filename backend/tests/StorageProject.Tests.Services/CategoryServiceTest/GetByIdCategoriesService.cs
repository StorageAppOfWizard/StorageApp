using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class GetByIdCategoriesService :IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetByIdCategoriesService(CategoryServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GuidCategoryById_WhenIdExist_ReturnCategory()
        {
            //Arrange
            var category = new Category { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(category.Id, cancellationToken)).ReturnsAsync(category);

            //Act
            var result = await _fixture.Service.GetByIdAsync(category.Id);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }
        
        [Fact]
        public async Task GuidCategoryById_WhenIdNoExist_ErrorReturnCategory()
        {
            //Arrange
            var category = new Category { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(category.Id, cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetByIdAsync(category.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }


    }
}
