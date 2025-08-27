using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class DeleteCategoryTest :IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        public DeleteCategoryTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteCategory_WhenIdIsAvailable_DeleCategory()
        {
            //Arrange
            var Category = new Category { Id = Guid.NewGuid(), Name ="Teste", Products = []};   
            
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(Category.Id, cancellationToken)).ReturnsAsync(Category);

            //Act
            var result = await _fixture.Service.RemoveAsync(Category.Id);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Fact]
        public async Task DeleteCategory_WhenIdIsUnavailable_ErrorDeleCategory()
        {
            //Arrange
            var Category = new Category { Id = Guid.NewGuid(), Name = "Teste", Products = [] };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(Category.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.RemoveAsync(Category.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
