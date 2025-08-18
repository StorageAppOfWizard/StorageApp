using Moq;
using StorageProject.Application.Contracts;
using StorageProject.Api.Controllers;
namespace StorageProject.Tests.CategoryControllerTest
{
    public class CategoryControllerFixture
    {
        public Mock<ICategoryService> CategoryServiceMock { get; } = new();
        public CategoryController Controller { get; }


        public CategoryControllerFixture()
        {
            Controller = new CategoryController(CategoryServiceMock.Object);
        }
    }
}
