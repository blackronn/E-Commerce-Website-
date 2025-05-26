using E_Commerce.Application.Dtos.OrderItemDtos;
using E_Commerce.Application.Usecases.OrderItemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemServices _orderItemServices;
        public OrderItemsController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }
        [HttpGet("GetAllOrderItem")]
        public async Task<IActionResult> GetAllOrderItem()
        {
            var values = await _orderItemServices.GetAllOrderItemAsync();
            return Ok(values);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _orderItemServices.GetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost("CreateOrderItem")]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDto model)
        {
            await _orderItemServices.CreateOrderItemAsync(model);
            return Ok();
        }
        [HttpPut("UpdateOrderItem")]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemDto model)
        {
            await _orderItemServices.UpdateOrderItemAsync(model);
            return Ok();
        }
        [HttpDelete("DeleteOrderItem/{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemServices.DeleteOrderItemAsync(id);
            return Ok();
        }
    }

}
