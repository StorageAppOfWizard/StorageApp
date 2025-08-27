using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class GetByIdCategoriesService :IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public GetByIdCategoriesService(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GuidBrandById_WhenIdExist_ReturnBrand()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(brand.Id, cancellationToken)).ReturnsAsync(brand);

            //Act
            var result = await _fixture.Service.GetByIdAsync(brand.Id);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }
        
        [Fact]
        public async Task GuidBrandById_WhenIdNoExist_ReturnBrand()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Teste" };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(brand.Id, cancellationToken)).ReturnsAsync(value:null);

            //Act
            var result = await _fixture.Service.GetByIdAsync(brand.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }


    }
}
