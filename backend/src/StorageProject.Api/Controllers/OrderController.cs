using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Extensions;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Api.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            
        }

        [HttpPost("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] Guid id, OrderStatus status)
        {
            var result = await _orderService.CancelOrderAsync(id, status);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO order)
        {
            var result  = await _orderService.CreateAsync(order);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _orderService.GetAllAsync();
            return result.ToActionResult();

        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result  = await _orderService.DeleteAsync(id);
            return result.ToActionResult();
        }

    }
}
