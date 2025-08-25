using Ardalis.Result;
using Moq;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Entity;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class CreateBrandTest : IClassFixture<BrandServiceFixture>
    {
        private readonly BrandServiceFixture _fixture;

        private readonly CancellationToken cancellationToken = CancellationToken.None;
        public CreateBrandTest(BrandServiceFixture fixture)
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

            //to do: fix test cause the return is null and crash the test in DTO mapper
            _fixture.UnitOfWorkMock.Setup(c => c.
                BrandRepository.GetByNameAsync(dto.Name, cancellationToken))
                               .ReturnsAsync(value: null);

            _fixture.UnitOfWorkMock.Setup(c => c
            .BrandRepository.Create(It.IsAny<Brand>(), cancellationToken))
                            .ReturnsAsync(newBrand);

            _fixture.UnitOfWorkMock.Setup(c => c.CommitAsync());

            var result = await _fixture.Service.CreateAsync(dto);
            var objectResult = Assert.IsType<BrandDTO>(result.Value);

            Assert.Equal(dto.Name, objectResult.Name);
        }
        
        [Fact]
        public async Task CreateBrand_WhenNameFieldIncorrect_ReturnBadRequestresult()
        {
            //Arrange
            var dto = new CreateBrandDTO { Name = "" };

            //Act
            var result = await _fixture.Service.CreateAsync(dto); 
           
            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(result, Result.Invalid());
        }

    }
}
