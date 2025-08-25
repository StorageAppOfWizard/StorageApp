using Moq;
using StorageProject.Application.Services;
using StorageProject.Domain.Contracts;

namespace StorageProject.Tests.Services.BrandServiceTest
{
    public class BrandServiceFixture
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();
        public BrandService Service { get; protected set; }

        public BrandServiceFixture()
        {
            Service = new BrandService(UnitOfWorkMock.Object);
        }
    }
}