using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Order;

namespace StorageProject.Tests.Controller.OrderControllerTest
{
    public class CreateOrderTest : IClassFixture<OrderControllerFixture>
    {
        private readonly OrderControllerFixture _fixture;

        public CreateOrderTest(OrderControllerFixture fixture)
        {
            _fixture = fixture;
            _fixture.OrderServiceMock.Reset();
        }

        [Fact]
        public async Task CreateOrder_ProductAvailable_ReturnOkResult()
        {
            //Arrange
            _fixture.OrderServiceMock.Setup
                (
                s => s
                .CreateOrderAsync(
                It.IsAny<CreateOrderDTO>()))
                .ReturnsAsync(Result.Success());

            // Act 
            var result = await _fixture.Controller.Create(It.IsAny<CreateOrderDTO>());

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s =>s.CreateOrderAsync(It.IsAny<CreateOrderDTO>()), Times.Once);
            

        }

        [Fact]
        public async Task CreateOrder_ProductUnaivalable_ReturnBadRequestResult()
        {
            var dto = new CreateOrderDTO { ProductId = Guid.NewGuid(), Quantity = 100 };
            _fixture.OrderServiceMock.Setup
                (s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>())).ReturnsAsync(Result.Error());

            //Act
            var result = await _fixture.Controller.Create(dto);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>()), Times.Once);

        }

        [Fact]
        public async Task CreateOrder_ProductNotExist_ReturnNotFoundResult()
        {
            var dto = new CreateOrderDTO { ProductId = Guid.Empty, Quantity = 100 };
            _fixture.OrderServiceMock.Setup
                (s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>())).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.Create(dto);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_ReturnInternalServerErrorResult()
        {
            _fixture.OrderServiceMock.Setup
                (s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>())).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.Create(It.IsAny<CreateOrderDTO>()));

            //Assert
            var objectResult = Assert.IsType<Exception>(exception);
            //Assert
            Assert.Equal("Unexpected Error", exception.Message);

            _fixture.OrderServiceMock.Verify(s => s.CreateOrderAsync(It.IsAny<CreateOrderDTO>()), Times.Once);
        }
    }
}
