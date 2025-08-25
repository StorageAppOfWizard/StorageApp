using Ardalis.Result;
using Moq;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class DeleteBrandTest :IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        public DeleteBrandTest(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteBrand_WhenIdIsAvailable_DeleBrand()
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
    }
}
