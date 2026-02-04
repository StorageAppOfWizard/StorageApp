using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class GetAllCategoriesServiceTest : IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetAllCategoriesServiceTest(CategoryServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllCategory_WhenCategorysExist_ReturnAllCategorys()
        {
            //Arrange
            var list = new List<Category> { 
                new Category { Name = "Teste",Id = Guid.NewGuid(), },
                new Category { Name = "Teste1",Id = Guid.NewGuid() },
                new Category { Name = "Teste2",Id = Guid.NewGuid() },
            };

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetAll(cancellationToken)).ReturnsAsync(list);

            //Act
            var result = await _fixture.Service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }

        [Fact]
        public async Task GetAllCategory_WhenCategorysNoExist_ReturnAllCategorys()
        {
            //Arrange

            _fixture.UnitOfWorkMock.Setup(c => c.CategoryRepository.GetAll(cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }
    }
}
