using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class GetAllCategoriesServiceTest : IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetAllCategoriesServiceTest(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllBrand_WhenBrandsExist_ReturnAllBrands()
        {
            //Arrange
            var list = new List<Brand> { 
                new Brand { Name = "Teste",Id = Guid.NewGuid(), },
                new Brand { Name = "Teste1",Id = Guid.NewGuid() },
                new Brand { Name = "Teste2",Id = Guid.NewGuid() },
            };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetAll(cancellationToken)).ReturnsAsync(list);

            //Act
            var result = await _fixture.Service.GetAllAsync();

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }

        [Fact]
        public async Task GetAllBrand_WhenBrandsNoExist_ErrorReturnAllBrands()
        {
            //Arrange

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetAll(cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetAllAsync();

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);

        }
    }
}
