using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Extensions;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;

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

        [HttpPatch("reject-order/{id:Guid}")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var result = await _orderService.RejectOrderAsync(id);
            return result.ToActionResult();
        }

        [HttpPatch("approve-order/{id:Guid}")]
        public async Task<IActionResult> ApproveOrder(Guid id)
        {
            var result = await _orderService.ApproveOrderAsync(id);
            return result.ToActionResult();
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO order)
        {
            var result  = await _orderService.CreateOrderAsync(order);
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDTO order)
        {
            var result = await _orderService.UpdateOrderAsync(order);
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
            var result  = await _orderService.DeleteOrderAsync(id);
            return result.ToActionResult();
        }

    }
}
