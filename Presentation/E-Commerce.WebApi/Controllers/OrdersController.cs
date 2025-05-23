using E_Commerce.Application.Dtos.OrderDtos;
using E_Commerce.Application.Usecases.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _orderServices.GetAllOrderAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _orderServices.GetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto model)
        {
            await _orderServices.CreateOrderAsync(model);
            return Ok("Order Created Successfully");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderDto model)
        {
            await _orderServices.UpdateOrderAsync(model);
            return Ok("Order Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderServices.DeleteOrderAsync(id);
            return Ok("Order Successfully Deleted");
        }
         
    }
}
