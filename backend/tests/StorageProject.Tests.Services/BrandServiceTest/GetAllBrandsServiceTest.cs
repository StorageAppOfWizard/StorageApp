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
            int page = 1;
            int pageQuantity = 10;
            var result = await _fixture.Service.GetAllAsync(page, pageQuantity);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }

        [Fact]
        public async Task GetAllBrand_WhenBrandsNoExist_ReturnAllBrands()
        {
            //Arrange

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetAll(cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetAllAsync(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

        }
    }
}
