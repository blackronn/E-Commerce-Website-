using E_Commerce.Application.Dtos.CustomerDtos;
using E_Commerce.Application.Usecases.CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;
        public CustomersController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _customerServices.GetAllCustomerAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _customerServices.GetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto model)
        {
            await _customerServices.CreateCustomerAsync(model);
            return Ok("Customer Created Successfully");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerDto model)
        {
            await _customerServices.UpdateCustomerAsync(model);
            return Ok("Customer Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerServices.DeleteCustomerAsync(id);
            return Ok("Customer Successfully Deleted");
        }
    }
}
