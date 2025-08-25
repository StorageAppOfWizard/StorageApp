using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class UpdateBrandTest : IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public UpdateBrandTest(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateBrand_WhenFieldsAreCorrect_BrandUpdated()
        {
            //Arrange
            var dto = new UpdateBrandDTO { Name = "TesteUpdate", Id = Guid.NewGuid() };
            var brandDto = new BrandDTO { Id = dto.Id, Name = dto.Name };
            var newBrand = new Brand { Id = brandDto.Id, Name = brandDto.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.
                BrandRepository.GetByNameAsync(dto.Name, cancellationToken))
                               .ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(newBrand);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateBrand_WhenConflictExist_BrandNotUpdated()
        {
            //Arrange
            var dto = new UpdateBrandDTO { Id = Guid.NewGuid(), Name = "TesteUpdate" };
            var newBrand = new Brand { Id = dto.Id, Name = dto.Name };
            var existingBrandName = new Brand { Id = Guid.NewGuid(), Name = newBrand.Name };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(It.IsAny<Brand>);
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(newBrand);
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(existingBrandName);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.Conflict, result.Status);
        }

        public async Task UpdateBrand_WhenBrandNotExist_BrandNotUpdated()
        {
            //Arrange
            var dto = new UpdateBrandDTO { Id = Guid.NewGuid(), Name = "TesteUpdate" };

            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetByNameAsync(dto.Name, cancellationToken)).ReturnsAsync(It.IsAny<Brand>);
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.GetById(dto.Id, cancellationToken)).ReturnsAsync(value: null);

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a brand name that should be max 20 chars...")]

        public async Task UpdateBrand_WhenNameFieldIsIncorret_InvalidName(string invalidName)
        {
            //Arrange
            var dto = new UpdateBrandDTO { Name = invalidName, Id = Guid.NewGuid() };

            //Act
            var result = await _fixture.Service.UpdateAsync(dto);

            //Assert
            Assert.False(result.IsSuccess);
        }


    }
}
