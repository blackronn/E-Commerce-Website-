using E_Commerce.Application.Dtos.CartItemDtos;
using E_Commerce.Application.Usecases.CartItemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemServices _cartItemServices;
        public CartItemController(ICartItemServices cartItemServices)
        {
            _cartItemServices = cartItemServices;
        }
        [HttpGet("GetAllCartItem")]
        public async Task<IActionResult> GetAllCartItem()
        {
            var result = await _cartItemServices.GetAllCartItemAsync();
            return Ok(result);
        }
        [HttpGet("GetCartItemById/{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var result = await _cartItemServices.GetCartItemByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("CreateCartItem")]
        public async Task<IActionResult> CreateCartItem(CreateCartItemDto createCartItemDto)
        {
            await _cartItemServices.CreateCartItemAsync(createCartItemDto);
            return Ok("Cart Item Created Successfully");
        }
        [HttpPut("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto updateCartItemDto)
        {
            await _cartItemServices.UpdateCartItemAsync(updateCartItemDto);
            return Ok("Cart Item Updated Successfully");
        }
        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _cartItemServices.DeleteCartItemAsync(id);
            return Ok("Cart Item Deleted Successfully");


        }
    }   
}
