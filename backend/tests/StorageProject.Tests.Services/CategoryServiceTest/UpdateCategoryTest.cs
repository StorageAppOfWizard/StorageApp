using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class UpdateCategoryTest : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public UpdateCategoryTest(ProductServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateCategory_WhenFieldsAreCorrect_CategoryUpdated()
        {
            //Arrange
            var dto = new UpdateCategoryDTO { Name = "TesteUpdate", Id = Guid.NewGuid() };
            var CategoryDto = new CategoryDTO { Id = dto.Id, Name = dto.Name };
            var newCategory = new Category { Id = CategoryDto.Id, Name = CategoryDto.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.
                CategoryRepository.GetByNameAsync(dto.Name, cancellationToken))
                               .ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(newCategory);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateCategory_WhenConflictExist_CategoryNotUpdated()
        {
            //Arrange
            var dto = new UpdateCategoryDTO { Id = Guid.NewGuid(), Name = "TesteUpdate" };
            var newCategory = new Category { Id = dto.Id, Name = dto.Name };
            var existingCategoryName = new Category { Id = Guid.NewGuid(), Name = newCategory.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(It.IsAny<Category>);
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(newCategory);
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(existingCategoryName);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.Conflict, result.Status);
        }

        public async Task UpdateCategory_WhenCategoryNotExist_CategoryNotUpdated()
        {
            //Arrange
            var dto = new UpdateCategoryDTO { Id = Guid.NewGuid(), Name = "TesteUpdate" };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(It.IsAny<Category>);
            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a Category name that should be max 20 chars...")]

        public async Task UpdateCategory_WhenNameFieldIsIncorret_InvalidName(string invalidName)
        {
            //Arrange
            var dto = new UpdateCategoryDTO { Name = invalidName, Id = Guid.NewGuid() };

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
        }


    }
}
