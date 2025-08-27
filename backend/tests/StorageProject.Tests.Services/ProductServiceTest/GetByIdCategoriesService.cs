using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class GetByIdCategoriesService :IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetByIdCategoriesService(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GuidCategoryById_WhenIdExist_ReturnCategory()
        {
            //Arrange
            var Category = new Category { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(Category.Id, cancellationToken)).ReturnsAsync(Category);

            //Act
            var result = await _fixture.Service.GetByIdAsync(Category.Id);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }
        
        [Fact]
        public async Task GuidCategoryById_WhenIdNoExist_ReturnCategory()
        {
            //Arrange
            var Category = new Category { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(Category.Id, cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetByIdAsync(Category.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }


    }
}
