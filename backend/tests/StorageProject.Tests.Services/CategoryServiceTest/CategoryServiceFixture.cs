using Moq;
using StorageProject.Application.Services;
using StorageProject.Domain.Contracts;

namespace StorageProject.Tests.Services.CategoryServiceTest
{
    public class CategoryServiceFixture
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();
        public CategoryService Service { get; protected set; }

        public CategoryServiceFixture()
        {
            Service = new CategoryService(UnitOfWorkMock.Object);
        }
    }
}