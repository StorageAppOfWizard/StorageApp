using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageProject.Tests.Controller.OrderControllerTest
{
    public class ApproveOrderTest : IClassFixture<OrderControllerFixture>
    {
        private readonly OrderControllerFixture _fixture;

        public ApproveOrderTest(OrderControllerFixture fixture)
        {
            _fixture = fixture;
            _fixture.OrderServiceMock.Reset();
        }

        [Fact]
        public async Task ApproveOrder_ReturnOkResult()
        {

            // Arrange
            _fixture.OrderServiceMock.Setup(o => o.ApproveOrderAsync(It.IsAny<Guid>())).ReturnsAsync(Result.Success());

            //Act
            var result = await _fixture.Controller.ApproveOrder(It.IsAny<Guid>());

            //Assert 
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s => s.ApproveOrderAsync(It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async Task ApproveOrder_ReturnNotFoundResult()
        {
            // Arrange
            _fixture.OrderServiceMock.Setup(o => o.ApproveOrderAsync(It.IsAny<Guid>())).ReturnsAsync(Result.NotFound());

            //Act
            var result = await _fixture.Controller.ApproveOrder(It.IsAny<Guid>());

            //Assert 
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s => s.ApproveOrderAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task ApproveOrder_ReturnBadRequestResult()
        {
            // Arrange
            _fixture.OrderServiceMock.Setup(o => o.ApproveOrderAsync(It.IsAny<Guid>())).ReturnsAsync(Result.Invalid());

            //Act
            var result = await _fixture.Controller.ApproveOrder(It.IsAny<Guid>());

            //Assert 
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);

            _fixture.OrderServiceMock.Verify(s => s.ApproveOrderAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task ApproveOrder_ReturnInternalServerErrorResult()
        {
            _fixture.OrderServiceMock.Setup
                (s => s.ApproveOrderAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception("Unexpected Error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture.Controller.ApproveOrder(It.IsAny<Guid>()));

            //Assert
            var objectResult = Assert.IsType<Exception>(exception);
            //Assert
            Assert.Equal("Unexpected Error", exception.Message);

            _fixture.OrderServiceMock.Verify(s => s.ApproveOrderAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
