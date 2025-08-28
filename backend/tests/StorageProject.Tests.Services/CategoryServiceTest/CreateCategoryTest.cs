using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class CreateCategoryTest : IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _fixture;

        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public CreateCategoryTest(CategoryServiceFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task CreateCategory_WhenFieldsAreCorrect_CategoryCreated()
        {
            //Arrange
            var dto = new CreateCategoryDTO{ Name = "Teste" };
            var CategoryDto = new CategoryDTO{ Id=Guid.NewGuid(),Name = dto.Name };
            var newCategory = new Category { Id = CategoryDto.Id, Name = CategoryDto.Name };
            _fixture.UnitOfWorkMock.Setup(c => c.
                CategoryRepository.GetByNameAsync(dto.Name, cancellationToken))
                               .ReturnsAsync(value: null);

            _fixture.UnitOfWorkMock.Setup(c => c
            .CategoryRepository.Create(It.IsAny<Category>(), cancellationToken))
                            .ReturnsAsync(newCategory);
            _fixture.UnitOfWorkMock.Setup(c => c.CommitAsync());

            //Act
            var result = await _fixture.Service.CreateAsync(dto);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a Category name that should be max 20 chars...")]
        public async Task CreateCategory_WhenNameFieldIsIncorret_ErrorBrandCreated(string invalidName)
        {
            //Arrange
            var dto = new CreateCategoryDTO { Name = invalidName };

            //Act
            var result = await _fixture.Service.CreateAsync(dto); 
           
            //Assert
            Assert.False(result.IsSuccess);
        }







    }
}
