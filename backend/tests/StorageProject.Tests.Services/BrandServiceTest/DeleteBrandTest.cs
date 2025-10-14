using Ardalis.Result;
using Moq;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class DeleteCategoryTest :IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        public DeleteCategoryTest(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteBrand_WhenIdIsAvailable_DeleteBrand()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name ="Teste", Products = []};   
            
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(brand.Id, cancellationToken)).ReturnsAsync(brand);

            //Act
            var result = await _fixture.Service.RemoveAsync(brand.Id);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);
        }

        [Fact]
        public async Task DeleteBrand_WhenIdIsUnavailable_ErrorDeleteBrand()
        {
            //Arrange
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Teste", Products = [] };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(brand.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.RemoveAsync(brand.Id);

            //Arrange
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
