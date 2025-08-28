using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Entity;
using System.Runtime.CompilerServices;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class CreateCategoryTest : IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;

        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public CreateCategoryTest(BrandServiceFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task CreateBrand_WhenFieldsAreCorrect_BrandCreated()
        {
            //Arrange
            var dto = new CreateBrandDTO{ Name = "Teste" };
            var brandDto = new BrandDTO{ Id=Guid.NewGuid(),Name = dto.Name };
            var newBrand = new Brand { Id = brandDto.Id, Name = brandDto.Name };
            _fixture.UnitOfWorkMock.Setup(c => c.
                BrandRepository.GetByNameAsync(dto.Name, cancellationToken))
                               .ReturnsAsync(value: null);

            _fixture.UnitOfWorkMock.Setup(c => c
            .BrandRepository.Create(It.IsAny<Brand>(), cancellationToken))
                            .ReturnsAsync(newBrand);
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
        [InlineData("this name is wayyyyyyyyyyyyyyyyyyyyy too long for a brand name that should be max 20 chars...")]

        public async Task CreateBrand_WhenNameFieldIsIncorret_ErrorBrandCreated(string invalidName)
        {
            //Arrange
            var dto = new CreateBrandDTO { Name = invalidName };

            //Act
            var result = await _fixture.Service.CreateAsync(dto); 
           
            //Assert
            Assert.False(result.IsSuccess);
        }







    }
}
