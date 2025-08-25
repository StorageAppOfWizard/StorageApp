using StorageProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _fixture.UnitOfWorkMock.Setup(c => c.BrandRepository.);
    }
}
