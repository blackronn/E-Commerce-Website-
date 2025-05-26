using E_Commerce.Application.Dtos.CartDtos;
using E_Commerce.Application.Usecases.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }
        [HttpGet("GetAllCart")]
        public async Task<IActionResult> GetAllCart()
        {
            var result = await _cartServices.GetAllCartAsync();
            return Ok(result);
        }
        [HttpGet("GetCartById/{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var result = await _cartServices.GetCartByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart(CreateCartDto createCartDto)
        {
            await _cartServices.CreateCartAsync(createCartDto);
            return Ok("Cart Created Successfully");
        }
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart(UpdateCartDto updateCartDto)
        {
            await _cartServices.UpdateCartAsync(updateCartDto);
            return Ok("Cart Updated Successfully");
        }
        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _cartServices.DeleteCartAsync(id);
            return Ok("Cart Deleted Successfully");
        }
    }
}
